using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Payments;
using Finance.API.Payments.DeletePayment;
using Finance.API.Payments.ReleasePayment;
using MassTransit;

namespace Finance.API.Payments.EventHandler.Integration;

public class OrderCompletedEventHandler
    (ISender sender,
    ILogger<OrderCompletedEventHandler> logger)
    : IConsumer<OrderCompletedEvent>
{
    public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = MapToReleasePaymentCommand(context.Message);
        await sender.Send(command);
    }

    private ReleasePaymentCommand MapToReleasePaymentCommand(OrderCompletedEvent message) {
        return new ReleasePaymentCommand(
            message.OrderId,
            message.TotalPrice
        );
        
    }
}

