using Finance.API.Exceptions;

namespace Finance.API.Payments.RefundPayment;

public record RefundPaymentCommand(Guid OrderId, decimal TotalPrice)
    : ICommand<RefundPaymentResult>;

public record RefundPaymentResult(bool IsSuccess);

public class RefundPaymentCommandHandler
    (IDocumentSession session)
    : ICommandHandler<RefundPaymentCommand, RefundPaymentResult>
{
    public async Task<RefundPaymentResult> Handle(RefundPaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = await session.Query<Payment>().Where(x => x.OrderId == command.OrderId).FirstOrDefaultAsync();

        if (payment == null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        var customer = await session.LoadAsync<Account>(payment.CustomerId, cancellationToken);

        if (customer == null)
        {
            throw new AccountNotFoundException(payment.CustomerId);
        }

        customer.Balance += payment.TotalPrice;

        // save to database
        session.Store(customer);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new RefundPaymentResult(true);
    }
}
