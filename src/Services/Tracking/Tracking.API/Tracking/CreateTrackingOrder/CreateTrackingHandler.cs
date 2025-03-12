using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;



namespace Tracking.API.Tracking;

public record CreateTrackingCommand(OrderStatus orderStatus)
    : ICommand<CreateTrackingResult>;

public record CreateTrackingResult(Guid OrderId);

public class CreateTrackingCommandHandler : ICommandHandler<CreateTrackingCommand, CreateTrackingResult>
{

    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IDocumentSession _session;
    private readonly TrackingMoreService _trackingMoreService;
    private readonly ILogger<CreateTrackingCommandHandler> _logger;

    public CreateTrackingCommandHandler(
        IPublishEndpoint publishEndpoint,
        IDocumentSession session,
        TrackingMoreService trackingMoreService,
        ILogger<CreateTrackingCommandHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _session = session;
        _trackingMoreService = trackingMoreService;
        _logger = logger;
    }
    public async Task<CreateTrackingResult> Handle(CreateTrackingCommand command, CancellationToken cancellationToken)
    {
         _logger.LogInformation($"Creating tracking order for {command.orderStatus.TrackingId}...");

        var orderStatus = new OrderStatus
        {
            OrderId = command.orderStatus.OrderId,
            TrackingId = command.orderStatus.TrackingId,
            CreatedAt = DateTime.UtcNow,
            Courier = command.orderStatus.Courier,
            Status = command.orderStatus.Status,
            UpdateTime = DateTime.UtcNow,
            Name = command.orderStatus.Name
        };

        var orderStatusDto = MapToOrderStatusDto(orderStatus);

        // save to database
        _session.Store(orderStatus);
        await _session.SaveChangesAsync(cancellationToken);

        // Send tracking order to AfterShip API
        var trackingMoreResponse = await _trackingMoreService.CreateTrackingOrder(orderStatusDto);
        _logger.LogInformation($"TrackingMore Response: {trackingMoreResponse}");

        var eventMessage = orderStatus.Adapt<OrderStatusCreatedEvent>();
        await _publishEndpoint.Publish(eventMessage, cancellationToken);

        // return result
        return new CreateTrackingResult(orderStatus.OrderId);
    }

    private static OrderStatusDto MapToOrderStatusDto(OrderStatus orderStatus)
    {
        return new  OrderStatusDto
        {
            TrackingId = orderStatus.TrackingId.ToString(),
            NewStatus = orderStatus.Status,
            Courier = orderStatus.Courier,
            OrderId = orderStatus.OrderId.ToString(),
            CreatedAt = orderStatus.CreatedAt,
            CustomerName = orderStatus.Name
        };
    }

    private string? ExtractTrackingMoreId(string trackingmoreResponse)
    {
        try
        {
            using var doc = JsonDocument.Parse(trackingmoreResponse);
            var root = doc.RootElement;

            if (root.TryGetProperty("data", out JsonElement dataElement) &&
                dataElement.TryGetProperty("id", out JsonElement idElement))
            {
                string trackingMoreId = idElement.GetString();
                _logger.LogInformation($"TrackingMore ID: {trackingMoreId}");
                return trackingMoreId;
            }
            else
            {
                _logger.LogWarning("TrackingMore response does not contain 'id'.");
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError($"Error parsing TrackingMore response: {ex.Message}");
        }

        return null; // Return null if extraction fails
    }

}
