namespace Tracking.API.Tracking;

public record GetTrackingByIdResponse(OrderStatus? OrderStatus);

public class GetTrackingByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
 
        // ✅ New GET tracking order by ID
        app.MapGet("/tracking/{trackingId:guid}", async (Guid trackingId, ISender sender) =>
        {
            var query = new GetTrackingByIdQuery(trackingId.ToString());
            var result = await sender.Send(query);

            if (result.OrderStatus == null)
            {
                return Results.NotFound(new { Message = $"Tracking order with ID {trackingId} not found." });
            }

            var response = result.Adapt<GetTrackingByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetTrackingById")
        .Produces<GetTrackingByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Tracking Order by ID")
        .WithDescription("Fetches a tracking order by its unique tracking ID.");
    }
}
