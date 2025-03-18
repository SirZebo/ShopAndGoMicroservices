using Feedback.API.Model;

namespace Feedback.API.Disputes.GetDisputesByAccepted;

public record GetDisputesByAcceptedRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetDisputesByAcceptedResponse(IEnumerable<Review> Reviews);
public class GetDisputesByAcceptedEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/disputes/accepted", async ([AsParameters] GetDisputesByAcceptedRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetDisputesByAcceptedQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetDisputesByAcceptedResponse>();

            return Results.Ok(response);
        })
        .WithName("GetDisputesByAccepted")
        .Produces<GetDisputesByAcceptedResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Disputes By Accepted")
        .WithDescription("Get Disputes By Accepted");

    }
}
