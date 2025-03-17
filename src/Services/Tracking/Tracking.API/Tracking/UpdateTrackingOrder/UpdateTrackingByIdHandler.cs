using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public record UpdateTrackingByIdCommand(string trackingId, UpdateTrackingDto updateTrackingDto) : ICommand<UpdateTrackingByIdResult>;

public record UpdateTrackingByIdResult(bool IsUpdated);

internal class UpdateTrackingByIdHandler : ICommandHandler<UpdateTrackingByIdCommand, UpdateTrackingByIdResult>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IDocumentSession _session;
    private readonly TrackingMoreService _trackingMoreService;
    private readonly ILogger<UpdateTrackingByIdHandler> _logger;


    public UpdateTrackingByIdHandler(
        IPublishEndpoint publishEndpoint,
        IDocumentSession session,
        TrackingMoreService trackingMoreService,
        ILogger<UpdateTrackingByIdHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _session = session;
        _trackingMoreService = trackingMoreService;
        _logger = logger;
    }


    public async Task<UpdateTrackingByIdResult> Handle(UpdateTrackingByIdCommand command, CancellationToken cancellationToken)
    {

        var trackingMoreId = await GetTrackingMoreIdAsync(command.trackingId);

        _logger.LogInformation($"trackingMoreId GET: {trackingMoreId}");


        if (string.IsNullOrEmpty(trackingMoreId))
        {
            _logger.LogWarning($"No TrackingMore ID found for Tracking Number: {command.trackingId}");
            return new UpdateTrackingByIdResult(false);
        }

        Guid.TryParse(command.trackingId, out var trackingIdGuid);

        var order = await _session.Query<OrderStatus>()
                          .FirstOrDefaultAsync(o => o.TrackingId == trackingIdGuid, cancellationToken);

        if (order == null)
        {
            return new UpdateTrackingByIdResult(false);
        }

        order.FirstName = command.updateTrackingDto.FirstName;
        order.LastName = command.updateTrackingDto.LastName;
        order.Status = command.updateTrackingDto.NewStatus;


        _logger.LogInformation($"Updating TrackingMore Order ID: {trackingMoreId}");

        var UpdateSuccess = await _trackingMoreService.UpdateTrackingOrder(trackingMoreId,command.updateTrackingDto);

        //  Update from PostgreSQL
        _session.Update(order);
        await _session.SaveChangesAsync(cancellationToken);

        return new UpdateTrackingByIdResult(true);
    }

    private async Task<string?> GetTrackingMoreIdAsync(string trackingNumber)
    {
        try
        {
            var responseContent = await _trackingMoreService.GetTrackingOrder(trackingNumber);

            _logger.LogInformation($"Get DATA: {responseContent}");

            using var doc = JsonDocument.Parse(responseContent);
            var root = doc.RootElement;

            if (root.TryGetProperty("data", out JsonElement dataElement))
            {
                if (dataElement.ValueKind == JsonValueKind.Object)
                {
                    // Single tracking result
                    if (dataElement.TryGetProperty("id", out JsonElement idElement))
                    {
                        return idElement.GetString();
                    }
                }
                else if (dataElement.ValueKind == JsonValueKind.Array)
                {
                    // Multiple tracking results
                    foreach (var element in dataElement.EnumerateArray())
                    {
                        if (element.TryGetProperty("id", out JsonElement idElement))
                        {
                            return idElement.GetString(); // Return the first ID found
                        }
                    }
                }
            }

            _logger.LogWarning($"No 'id' found in TrackingMore response for tracking number: {trackingNumber}");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error extracting TrackingMore ID: {ex.Message}");
            return null;
        }
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
