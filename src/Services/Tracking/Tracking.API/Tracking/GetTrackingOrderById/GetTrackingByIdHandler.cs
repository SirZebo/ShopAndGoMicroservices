using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;


namespace Tracking.API.Tracking;

public record GetTrackingByIdQuery(Guid TrackingId) : IQuery<GetTrackingByIdResult>;

public record GetTrackingByIdResult(Models.OrderStatus? OrderStatus);

internal class GetTrackingByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetTrackingByIdQuery, GetTrackingByIdResult>
{
    public async Task<GetTrackingByIdResult> Handle(GetTrackingByIdQuery query, CancellationToken cancellationToken)
    {
        var orderStatus = await session.Query<OrderStatus>()
                                       .FirstOrDefaultAsync(o => o.TrackingId == query.TrackingId, cancellationToken);

        return new GetTrackingByIdResult(orderStatus);
    }
}