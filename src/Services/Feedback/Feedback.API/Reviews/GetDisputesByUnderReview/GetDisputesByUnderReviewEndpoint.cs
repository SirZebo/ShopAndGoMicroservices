using Feedback.API.Model;

namespace Feedback.API.Reviews.GetDisputes;

public record GetDisputesByUnderReviewRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetDisputesByUnderReviewResponse(IEnumerable<Review> Reviews);
public class GetDisputesByUnderReviewEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/disputes", async ([AsParameters] GetDisputesByUnderReviewRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetDisputesByUnderReviewQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetDisputesByUnderReviewResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetDisputesByUnderReviewResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");

    }
}
