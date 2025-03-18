using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Disputes;
using Feedback.API.Enums;
using Feedback.API.Exceptions;
using Feedback.API.Model;
using MassTransit;

namespace Feedback.API.Disputes.UpdateDispute;

public record UpdateDisputeCommand(Guid Id, DisputeStatus DisputeStatus) : ICommand<UpdateDisputeResult>;

public record UpdateDisputeResult(bool IsSuccess);

public class UpdateDisputeValidator : AbstractValidator<UpdateDisputeCommand>
{
    public UpdateDisputeValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Review Id is required");

        RuleFor(x => x.DisputeStatus)
            .NotEmpty()
            .NotEqual(DisputeStatus.UnderReview).WithMessage("Dispute Status cannot be under review")
            .NotEqual(DisputeStatus.NoDispute).WithMessage("Dispute Status cannot be no dispute");
    }
}

internal class UpdateDisputeCommandHandler
    (IDocumentSession session,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateDisputeCommand, UpdateDisputeResult>
{
    public async Task<UpdateDisputeResult> Handle(UpdateDisputeCommand command, CancellationToken cancellationToken)
    {
        var review = await session.LoadAsync<Review>(command.Id, cancellationToken);

        if (review is null)
        {
            throw new ReviewNotFoundException(command.Id);
        }

        review.DisputeStatus = command.DisputeStatus;

        session.Update(review);
        await session.SaveChangesAsync(cancellationToken);

        if (review.DisputeStatus == DisputeStatus.Accepted)
        {
            var eventMessage = new DisputeAcceptedEvent { OrderId = review.Order.Id };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
        }
        if (review.DisputeStatus == DisputeStatus.Rejected)
        {
            var eventMessage = new DisputeRejectedEvent { OrderId = review.Order.Id };
            await publishEndpoint.Publish(eventMessage, cancellationToken);
        }

        return new UpdateDisputeResult(true);
    }
}
