namespace Shipping.API.Shipping.UpdateShipment;

public record UpdateShipmentRequest(Guid Id, string TrackingNumber);

public record UpdateShipmentResponse(bool IsSuccess);
public class UpdateShipmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/shipments", async (UpdateShipmentRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateShipmentCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateShipmentResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateShipment")
        .Produces<UpdateShipmentResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Shipment")
        .WithDescription("Update Shipment");
    }
}