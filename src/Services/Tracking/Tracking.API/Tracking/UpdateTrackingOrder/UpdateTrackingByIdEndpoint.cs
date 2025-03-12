using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using MediatR; // Ensure you have MediatR for ISender
using Mapster;
using Carter;
using Microsoft.AspNetCore.Http;


public class UpdateTrackingByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // PUT Update Tracking Order
        app.MapPut("/tracking/update/{trackingId}", async (string trackingId, UpdateTrackingDto updateTrackingDto, ISender sender) =>
        {
            var command = new UpdateTrackingByIdCommand(trackingId, updateTrackingDto);
            var result = await sender.Send(command);

            if (!result.IsUpdated)
            {
                return Results.Problem("Failed to update tracking order.", statusCode: 500);
            }

            return Results.Ok(new { message = "Tracking order updated successfully." });
        })
        .WithName("UpdateTrackingOrder")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithSummary("Update Tracking Order")
        .WithDescription("Updates tracking order details in the database.");
    }
}
