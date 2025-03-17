using Feedback.API.Enums;

namespace Feedback.API.Disputes.UpdateDispute;

public record UpdateDisputeRequest(Guid Id, DisputeStatus DisputeStatus);

public record UpdateDisputeResponse(bool IsSuccess);
public class UpdateDisputeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/disputes", async (UpdateDisputeRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateDisputeCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateDisputeResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateDispute")
        .Produces<UpdateDisputeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Dispute")
        .WithDescription("Update Dispute");
    }
}
