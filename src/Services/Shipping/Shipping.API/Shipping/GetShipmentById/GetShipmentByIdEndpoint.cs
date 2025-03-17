using Shipping.API.Model;

namespace Shipping.API.Shipping.GetShipmentById;

// public record GetShipmentByIdRequest();

public record GetShipmentByIdResponse(Shipment Shipment);

public class GetShipmentByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shipments/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetShipmentByIdQuery(id));

            var response = result.Adapt<GetShipmentByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetShipmentById")
        .Produces<GetShipmentByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Shipment By Id")
        .WithDescription("Get Shipment By Id");

    }
}