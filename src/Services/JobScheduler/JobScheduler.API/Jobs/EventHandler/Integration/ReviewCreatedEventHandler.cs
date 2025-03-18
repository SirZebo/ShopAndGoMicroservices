using BuildingBlocks.Messaging.Events.Review;
using JobScheduler.API.Data;
using JobScheduler.API.Models;
using MassTransit;

namespace JobScheduler.API.Jobs.EventHandler.Integration;

public class ReviewCreatedEventHandler
    (IMessageScheduler scheduler,
    ApplicationDbContext dbContext,
    ILogger<PaymentCreatedEventHandler> logger)
    : IConsumer<ReviewCreatedEvent>
{
    public async Task Consume(ConsumeContext<ReviewCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var eventMessage = new ReviewDeadlineExceededEvent { ReviewId = context.Message.ReviewId };
        var scheduledMessage = await scheduler.SchedulePublish(DateTime.UtcNow.AddDays(7), eventMessage);

        var review = new Review { Id = context.Message.ReviewId, TokenId = scheduledMessage.TokenId };
        await dbContext.Reviews.AddAsync(review);
        await dbContext.SaveChangesAsync();

        await Task.CompletedTask;
    }
}