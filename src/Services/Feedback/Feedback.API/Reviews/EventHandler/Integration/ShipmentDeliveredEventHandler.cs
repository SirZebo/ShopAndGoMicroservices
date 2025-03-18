using BuildingBlocks.Messaging.Events.Shipments;
using Feedback.API.Dtos;
using Feedback.API.Reviews.CreateReview;
using MassTransit;

namespace Feedback.API.Reviews.EventHandler.Integration;

public class ShipmentDeliveredEventHandler
    (ISender sender, ILogger<ShipmentDeliveredEventHandler> logger)
    : IConsumer<ShipmentDeliveredEvent>
{
    public async Task Consume(ConsumeContext<ShipmentDeliveredEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateReviewCommand(context.Message);
        await sender.Send(command);
    }

    private CreateReviewCommand MapToCreateReviewCommand(ShipmentDeliveredEvent message)
    {
        var orderDto = new OrderDto
        {

        };

        return new CreateReviewCommand(orderDto);
    }

}