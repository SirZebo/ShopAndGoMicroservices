using BuildingBlocks.Messaging.Events;
using Finance.API.Payments.DeletePayment;
using MassTransit;

namespace Finance.API.Payments.EventHandler.Integration;

public class PaymentDeadlineExceededEventHandler
    (ISender sender,
    ILogger<PaymentDeadlineExceededEventHandler> logger)
    : IConsumer<PaymentDeadlineExceededEvent>
{
    public async Task Consume(ConsumeContext<PaymentDeadlineExceededEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var id = context.Message.PaymentId;
        var command = new DeletePaymentCommand(id);
        await sender.Send(command);
    }
}
