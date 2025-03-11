namespace Tracking.API.Tracking;

public record GetTrackingRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetTrackingResponse(IEnumerable<OrderStatus> OrderStatus);

public class GetTrackingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/tracking", async ([AsParameters] GetTrackingRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetOrderStatusQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetTrackingResponse>();

            return Results.Ok(response);
        })
        .WithName("GetTracking")
        .Produces<GetTrackingResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Tracking Order")
        .WithDescription("Get Tracking Order");

    }
}
