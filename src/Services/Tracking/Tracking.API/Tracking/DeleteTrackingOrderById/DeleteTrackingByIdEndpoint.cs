using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using MediatR; // Ensure you have MediatR for ISender
using Mapster;
using Carter;
using Microsoft.AspNetCore.Http;


public class TrackingModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // ✅ DELETE ALL Tracking Orders
        app.MapDelete("/tracking/delete/{trackingId:guid}", async (string trackingId,ISender sender) =>
        {
            var command = new DeleteTrackingByIdCommand(trackingId);
            var result = await sender.Send(command);

            if (!result.IsDeleted)
            {
                return Results.Problem("Failed to delete tracking order.", statusCode: 500);
            }

            return Results.Ok(new { message = "Tracking order deleted successfully." });
        })
        .WithName("DeleteTrackingById")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Delete Tracking Order")
        .WithDescription("Removes tracking order from the database.");
    }
}
