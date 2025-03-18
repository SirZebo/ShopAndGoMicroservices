using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Feedback.API.Reviews.EventHandler.Integration;

//public class ShipmentDeliveredEventHandler
//    (ISender sender, ILogger<ShipmentDeliveredEventHandler> logger)
//    : IConsumer<ShipmentDeliveredEvent>
//{
//    public async Task Consume(ConsumeContext<ShipmentDeliveredEvent> context)
//    {
//        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

//        var command = MapToCreateOrderCommand(context.Message);
//        await sender.Send(command);
//    }
//}