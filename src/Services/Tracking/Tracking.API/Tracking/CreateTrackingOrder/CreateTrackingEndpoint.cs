using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using MediatR; // Ensure you have MediatR for ISender
using Mapster;
using Carter;
using Microsoft.AspNetCore.Http;

namespace Tracking.API.Tracking;

public record CreateTrackingRequest(OrderStatus OrderStatus);
public record CreateTrackingResponse(Guid OrderId);

public class TrackingModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/tracking/create", async (CreateTrackingRequest request, ISender sender) =>
        {

            if (request == null || request.OrderStatus == null)
            {
                return Results.BadRequest(new { message = "Invalid request: OrderStatusDto cannot be null." });
            }

            // Convert request DTO to Command
            var command = request.Adapt<CreateTrackingCommand>();

            // Send command to MediatR
            var result = await sender.Send(command);

            // Convert result to response DTO
            var response = result.Adapt<CreateTrackingResponse>();

            return Results.Ok(response);
        })
        .WithName("CreateTracking")
        .Produces<CreateTrackingResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Tracking Entry")
        .WithDescription("Creates a tracking order and registers it with TrackingMore.");
    }
}
