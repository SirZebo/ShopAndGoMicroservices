using Catalog.API.Dtos;

namespace Catalog.API.Products.CheckoutProduct;

public record CheckoutProductRequest(CheckoutProductDto CheckoutProductDto);

public record CheckoutProductResponse(Guid TransactionToken);

public class CheckoutProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product/checkout", async (CheckoutProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutProductResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutProduct")
        .Produces<CheckoutProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Product")
        .WithDescription("Checkout Product");
    }
}
