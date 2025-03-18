using Shipping.API.Model;

namespace Shipping.API.Shipping.GetShipmentsByMerchantId;

public record GetShipmentsByCustomerIdQuery(Guid MerchantId, int? PageNumber = 1, int? PageSize = 10) : IQuery<GetShipmentsByCustomerIdResult>;

public record GetShipmentsByCustomerIdResult(IEnumerable<Shipment> Shipments);

internal class GetShipmentsByMerchantIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetShipmentsByCustomerIdQuery, GetShipmentsByCustomerIdResult>
{
    public async Task<GetShipmentsByCustomerIdResult> Handle(GetShipmentsByCustomerIdQuery query, CancellationToken cancellationToken)
    {
        var shipments = await session.Query<Shipment>()
                .Where(x => x.Order.MerchantId == query.MerchantId && 
                    x.ShipmentStatus == Enums.ShipmentStatus.OrderReceived)
                .OrderBy(x => x.Order.OrderDeadline)
                .ToListAsync();

        if (shipments.IsEmpty())
        {
            //throw new CustomerNotFoundException(query.CustomerId);
        }

        return new GetShipmentsByCustomerIdResult(shipments);
    }
}

