using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Finance.API.Payments.ReceivePayment;

public class PaymentReceivedEventHandler
    (ISender sender, ILogger<PaymentReceivedEventHandler> logger)
    : IConsumer<PaymentReceivedEvent>
{
    public async Task Consume(ConsumeContext<PaymentReceivedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = new ReceivePaymentCommand(context.Message.PaymentId);

        await sender.Send(command);
    }
}