using BuildingBlocks.Messaging.Events;
using Finance.API.Exceptions;
using Finance.API.Payments.ReceivePayment;
using MassTransit;

namespace Finance.API.Payments.MakePayment;

public record MakePaymentCommand(Guid PaymentId)
    : ICommand<MakePaymentResult>;

public record MakePaymentResult(bool IsSuccess);

public class MakePaymentHandlerCommandHandler
    (IPublishEndpoint publishEndpoint, IDocumentSession session, ISender sender)
    : ICommandHandler<MakePaymentCommand, MakePaymentResult>
{
    public async Task<MakePaymentResult> Handle(MakePaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = await session.LoadAsync<Payment>(command.PaymentId, cancellationToken);

        if (payment == null)
        {
            throw new PaymentNotFoundException(command.PaymentId);
        }
        var account = await session.LoadAsync<Account>(payment.CustomerId, cancellationToken);

        if (account == null)
        {
            throw new AccountNotFoundException(payment.CustomerId);
        }

        account.Balance -= payment.TotalPrice;
        if (account.Balance < 0)
        {
            throw new AccountInsufficientBalanceException(account.Id);
        }

        // save to database
        session.Store(account);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = new PaymentSucceededEvent
        {
            OrderId = payment.OrderId
        };
        await publishEndpoint.Publish(eventMessage);

        // return result
        return new MakePaymentResult(true);
    }
}