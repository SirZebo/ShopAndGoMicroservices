using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public record DeleteTrackingByIdCommand(string trackingId) : ICommand<DeleteTrackingByIdResult>;

public record DeleteTrackingByIdResult(bool IsDeleted);

internal class DeleteTrackingByIdHandler: ICommandHandler<DeleteTrackingByIdCommand, DeleteTrackingByIdResult>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IDocumentSession _session;
    private readonly TrackingMoreService _trackingMoreService;
    private readonly ILogger<DeleteTrackingByIdHandler> _logger;


    public DeleteTrackingByIdHandler(
        IPublishEndpoint publishEndpoint,
        IDocumentSession session,
        TrackingMoreService trackingMoreService,
        ILogger<DeleteTrackingByIdHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _session = session;
        _trackingMoreService = trackingMoreService;
        _logger = logger;
    }


    public async Task<DeleteTrackingByIdResult> Handle(DeleteTrackingByIdCommand command, CancellationToken cancellationToken)
    {

        var trackingMoreId = await GetTrackingMoreIdAsync(command.trackingId);

        _logger.LogInformation($"trackingMoreId GET: {trackingMoreId}");


        if (string.IsNullOrEmpty(trackingMoreId))
        {
            _logger.LogWarning($"No TrackingMore ID found for Tracking Number: {command.trackingId}");
            return new DeleteTrackingByIdResult(false);
        }

        _logger.LogInformation($"Deleting TrackingMore Order ID: {trackingMoreId}");

        var deleteSuccess = await _trackingMoreService.DeleteTrackingOrder(trackingMoreId);

        Guid.TryParse(command.trackingId, out var trackingIdGuid);
        //  Delete from PostgreSQL
        _session.DeleteWhere<OrderStatus>(x => x.TrackingId== trackingIdGuid);
        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteTrackingByIdResult(true);
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
