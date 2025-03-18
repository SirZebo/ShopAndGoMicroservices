using Feedback.API.Disputes.GetDisputesByAccepted;
using Feedback.API.Model;

namespace Feedback.API.Disputes.GetDisputesByRejected;

public record GetDisputesByRejectedRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetDisputesByRejectedResponse(IEnumerable<Review> Reviews);
public class GetDisputesByRejectedEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/disputes/rejected", async ([AsParameters] GetDisputesByRejectedRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetDisputesByRejectedQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetDisputesByRejectedResponse>();

            return Results.Ok(response);
        })
        .WithName("GetDisputesByRejected")
        .Produces<GetDisputesByRejectedResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Disputes By Rejected")
        .WithDescription("Get Disputes By Rejected");

    }
}