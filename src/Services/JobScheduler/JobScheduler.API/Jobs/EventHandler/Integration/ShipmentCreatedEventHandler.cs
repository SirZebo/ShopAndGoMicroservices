using BuildingBlocks.Messaging.Events.Shipments;
using JobScheduler.API.Data;
using JobScheduler.API.Models;
using MassTransit;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class ShipmentCreatedEventHandler
    (IMessageScheduler scheduler,
    ApplicationDbContext dbContext,
    ILogger<PaymentCreatedEventHandler> logger)
    : IConsumer<ShipmentCreatedEvent>
{
    public async Task Consume(ConsumeContext<ShipmentCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var eventMessage = new ShipmentDeadlineExceededEvent { ShipmentId = context.Message.ShipmentId };
        var scheduledMessage = await scheduler.SchedulePublish(context.Message.OrderDeadline, eventMessage);

        var shipment = new Shipment { Id = context.Message.ShipmentId, TokenId = scheduledMessage.TokenId };
        await dbContext.Shipments.AddAsync(shipment);
        await dbContext.SaveChangesAsync();

        await Task.CompletedTask;
    }
}