using BuildingBlocks.Messaging.Events.Review;
using Feedback.API.Dtos;
using Feedback.API.Enums;
using Feedback.API.Model;
using MassTransit;

namespace Feedback.API.Reviews.CreateReview;

public record CreateReviewCommand(OrderDto Order)
    : ICommand<CreateReviewResult>;

public record CreateReviewResult(Guid Id);

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
    }

}

internal class CreateReviewCommandHandler
    (IPublishEndpoint publishEndpoint,
    IDocumentSession session)
    : ICommandHandler<CreateReviewCommand, CreateReviewResult>
{
    // Business logic to create a product
    public async Task<CreateReviewResult> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        var order = command.Order.Adapt<Order>();
        var review = new Review
        {
            Id = Guid.NewGuid(),
            Order = order,
            FeedbackStatus = FeedbackStatus.Incomplete,
        };

        // save to database
        session.Store(review);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = new ReviewCreatedEvent
        {
            ReviewId = review.Id,
        };
        await publishEndpoint.Publish(eventMessage);

        // return result
        return new CreateReviewResult(review.Id);
    }
}