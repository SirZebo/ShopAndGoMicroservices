using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using MediatR; // Ensure you have MediatR for ISender
using Mapster;
using Carter;
using Microsoft.AspNetCore.Http;


public class TrackingDeleteModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // ✅ DELETE ALL Tracking Orders
        app.MapDelete("/tracking/delete-all", async (ISender sender) =>
        {
            var command = new DeleteAllTrackingCommand();
            var result = await sender.Send(command);

            if (!result.IsDeleted)
            {
                return Results.Problem("Failed to delete tracking orders.", statusCode: 500);
            }

            return Results.Ok(new { message = "All tracking orders deleted successfully." });
        })
        .WithName("DeleteAllTrackingOrders")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete All Tracking Orders")
        .WithDescription("Removes all tracking orders from the database.");
    }
}
