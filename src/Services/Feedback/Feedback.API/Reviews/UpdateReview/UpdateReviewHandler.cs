using BuildingBlocks.Messaging.Events.Review;
using Feedback.API.Enums;
using Feedback.API.Exceptions;
using Feedback.API.Model;
using MassTransit;


namespace Feedback.API.Reviews.UpdateReview;

public record UpdateReviewCommand(Guid Id, FeedbackStatus FeedbackStatus, string Body) : ICommand<UpdateReviewResult>;

public record UpdateReviewResult(bool IsSuccess);

public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Product Id is required");

        RuleFor(x => x.FeedbackStatus)
            .NotEmpty().WithMessage("FeedbackStatus is required");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Body is required")
            .Length(2, 150).WithMessage("Body must be between 2 and 150 characters");
    }
}

internal class UpdateReviewCommandHandler
    (IDocumentSession session,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateReviewCommand, UpdateReviewResult>
{
    public async Task<UpdateReviewResult> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
    {
        var review = await session.LoadAsync<Review>(command.Id, cancellationToken);

        if (review is null)
        {
            throw new ReviewNotFoundException(command.Id);
        }

        if (review.FeedbackStatus != FeedbackStatus.Incomplete)
        {
            throw new ReviewAlreadySubmittedException(command.Id);
        }

        review.FeedbackStatus = command.FeedbackStatus;
        review.Body = command.Body;
        review.LastModified = DateTime.UtcNow;
        if (review.FeedbackStatus == FeedbackStatus.Complaint)
        {
            review.DisputeStatus = DisputeStatus.UnderReview;
        }
        if (review.FeedbackStatus == FeedbackStatus.Satisfied)
        {
            var eventMessage = new ReviewSatisfiedEvent { OrderId = review.Order.Id };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
        }

        session.Update(review);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateReviewResult(true);
    }
}
