using Finance.API.Exceptions;
using Finance.API.Payments.MakePayment;
using Finance.API.Payments.ReceivePayment;

namespace Finance.API.Payments.ReleasePayment;

public record ReleasePaymentCommand(Guid OrderId, decimal TotalPrice)
    : ICommand<ReleasePaymentResult>;

public record ReleasePaymentResult(bool IsSuccess);

public class ReleasePaymentCommandHandler
    (IDocumentSession session)
    : ICommandHandler<ReleasePaymentCommand, ReleasePaymentResult>
{
    public async Task<ReleasePaymentResult> Handle(ReleasePaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = await session.Query<Payment>().Where(x => x.OrderId == command.OrderId).FirstOrDefaultAsync();

        if (payment == null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        var merchant = await session.LoadAsync<Account>(payment.MerchantId, cancellationToken);

        if (merchant == null)
        {
            throw new AccountNotFoundException(payment.MerchantId);
        }

        merchant.Balance += payment.TotalPrice;

        // save to database
        session.Store(merchant);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new ReleasePaymentResult(true);
    }
}
