using BuildingBlocks.Messaging.Events.Review;
using BuildingBlocks.Messaging.Events.Shipments;
using JobScheduler.API.Data;
using MassTransit;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class ReviewUpdatedEventHandler
    (IMessageScheduler scheduler,
    ApplicationDbContext dbContext,
    ILogger<ReviewUpdatedEventHandler> logger)
    : IConsumer<ReviewUpdatedEvent>
{
    public async Task Consume(ConsumeContext<ReviewUpdatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var review = await dbContext.Reviews.FindAsync(context.Message.ReviewId);
        if (review == null)
        {
            //
        }
        await scheduler.CancelScheduledPublish(typeof(ReviewDeadlineExceededEvent), review.TokenId);

        await Task.CompletedTask;
    }
}
