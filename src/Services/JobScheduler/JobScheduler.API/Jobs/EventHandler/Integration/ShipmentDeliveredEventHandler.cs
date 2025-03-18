using BuildingBlocks.Messaging.Events.Shipments;
using JobScheduler.API.Data;
using MassTransit;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class ShipmentDeliveredEventHandler
    (IMessageScheduler scheduler,
    ApplicationDbContext dbContext,
    ILogger<PaymentCreatedEventHandler> logger)
    : IConsumer<ShipmentDeliveredEvent>
{
    public async Task Consume(ConsumeContext<ShipmentDeliveredEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var shipment = await dbContext.Shipments.FindAsync(context.Message.ShipmentId);
        if (shipment == null) {
            //
        }
        await scheduler.CancelScheduledPublish(typeof(ShipmentDeadlineExceededEvent), shipment.TokenId);

        await Task.CompletedTask;
    }
}
