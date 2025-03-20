namespace Finance.API.Payments.ReceivePayment;

public record ReceivePaymentRequest(Guid PaymentId);

public record ReceivePaymentResponse(bool IsSuccess);

public class ReceivePaymentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/payments/receive", async (ReceivePaymentRequest request, ISender sender) =>
        {
            var command = request.Adapt<ReceivePaymentCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<ReceivePaymentResponse>();

            return Results.Ok(response);
        })
        .WithName("ReceivePayment")
        .Produces<ReceivePaymentResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Receive Payment")
        .WithDescription("Receive Payment");
    }
}
