using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;


namespace Tracking.API.Tracking;

public record GetOrderStatusQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetOrderStatusResult>;

public record GetOrderStatusResult(IEnumerable<Models.OrderStatus> OrderStatus);

internal class GetOrderStatusQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetOrderStatusQuery, GetOrderStatusResult>
{
    public async Task<GetOrderStatusResult> Handle(GetOrderStatusQuery query, CancellationToken cancellationToken)
    {
        var orderStatus = await session.Query<OrderStatus>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize?? 10, cancellationToken);

        return new GetOrderStatusResult(orderStatus);
    }
}

