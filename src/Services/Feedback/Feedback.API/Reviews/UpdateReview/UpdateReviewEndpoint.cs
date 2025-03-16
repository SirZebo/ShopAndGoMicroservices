using Feedback.API.Enums;

namespace Feedback.API.Reviews.UpdateReview;

public record UpdateReviewRequest(Guid Id, FeedbackStatus FeedbackStatus, string Body);

public record UpdateReviewResponse(bool IsSuccess);

public class UpdateReviewEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/reviews", async (UpdateReviewRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateReviewCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateReviewResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateReview")
        .Produces<UpdateReviewResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Review")
        .WithDescription("Update Review");
    }
}

