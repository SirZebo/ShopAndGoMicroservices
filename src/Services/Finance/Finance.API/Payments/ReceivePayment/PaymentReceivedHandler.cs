using Finance.API.Exceptions;
using Finance.API.Payments.MakePayment;
using MassTransit;

namespace Finance.API.Payments.ReceivePayment;

public record ReceivePaymentCommand(Guid PaymentId)
    : ICommand<ReceivePaymentResult>;

public record ReceivePaymentResult(bool IsSuccess);


public class ReceivePaymentCommandHandler
    (IDocumentSession session, ISender sender)
    : ICommandHandler<ReceivePaymentCommand, ReceivePaymentResult>
{
    public async Task<ReceivePaymentResult> Handle(ReceivePaymentCommand command, CancellationToken cancellationToken)
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

        account.Balance += payment.OutstandingAmount;

        // save to database
        session.Store(account);
        await session.SaveChangesAsync(cancellationToken);

        var makePaymentCommand = new MakePaymentCommand(payment.Id);
        await sender.Send(makePaymentCommand);

        // return result
        return new ReceivePaymentResult(true);
    }
}
