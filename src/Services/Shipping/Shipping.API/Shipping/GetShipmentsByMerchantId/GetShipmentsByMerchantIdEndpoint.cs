using Shipping.API.Model;

namespace Shipping.API.Shipping.GetShipmentsByMerchantId;

public record GetShipmentsByCustomerIdRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetShipmentsByCustomerIdResponse(List<Shipment> Shipments);

public class GetShipmentsByMerchantIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shipments/merchant/{id}", async ([AsParameters] GetShipmentsByCustomerIdRequest request, Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetShipmentsByCustomerIdQuery(id, request.PageNumber, request.PageSize));

            var response = result.Adapt<GetShipmentsByCustomerIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetShipmentByCustomerId")
        .Produces<GetShipmentsByCustomerIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Shipments By CustomerId")
        .WithDescription("Get Shipments By CustomerId");

    }
}