using BuildingBlocks.Messaging.Events;
using Finance.API.Dtos;
using MassTransit;

namespace Finance.API.Payments.CreatePayment;

public record CreatePaymentCommand(PaymentDto Payment)
    : ICommand<CreatePaymentResult>;

public record CreatePaymentResult(Guid Id);
public class CreatePaymentCommandHandler
{
}

public class CreatePaymentCommandHandler
    (IPublishEndpoint publishEndpoint, IDocumentSession session)
    : ICommandHandler<CreatePaymentCommand, CreatePaymentResult>
{
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            TransactionToken = command.Payment.TransactionToken,
            TotalPrice = command.Payment.TotalPrice,
        };

        // save to database
        session.Store(payment);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = payment.Adapt<PaymentCreatedEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // return result
        return new CreatePaymentResult(payment.Id);
    }

}
