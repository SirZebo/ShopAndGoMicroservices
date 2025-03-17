using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;  // Ensure correct using directive


namespace Tracking.API.Tracking
{
    public record GetTrackingByIdQuery(string TrackingId) : IQuery<GetTrackingByIdResult>;

    public record GetTrackingByIdResult(TrackingResponse? OrderStatus);

    internal class GetTrackingByIdQueryHandler
        : IQueryHandler<GetTrackingByIdQuery, GetTrackingByIdResult>
    {
        private readonly IDocumentSession _session;
        private readonly TrackingMoreService _trackingMoreService;

        // Constructor with dependency injection for _trackingMoreService and IDocumentSession
        public GetTrackingByIdQueryHandler(IDocumentSession session, TrackingMoreService trackingMoreService)
        {
            _session = session;
            _trackingMoreService = trackingMoreService;
        }

        public async Task<GetTrackingByIdResult> Handle(GetTrackingByIdQuery query, CancellationToken cancellationToken)
        {
            var trackingIdString = query.TrackingId.ToString();

            // Use the _trackingMoreService to get the tracking details
            var jsonResponse  = await _trackingMoreService.GetTrackingOrder(trackingIdString);

            var trackingData = JsonConvert.DeserializeObject<TrackingResponse>(jsonResponse);

            // Return the result with the OrderStatus
            return new GetTrackingByIdResult(trackingData);
        }
    }
}
