using Shipping.API.Model;

namespace Shipping.API.Shipping.GetShipmentById;

public record GetShipmentByIdQuery(Guid Id) : IQuery<GetShipmentByIdResult>;

public record GetShipmentByIdResult(Shipment Shipment);
internal class GetShipmentByIdHandlerQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetShipmentByIdQuery, GetShipmentByIdResult>
{
    public async Task<GetShipmentByIdResult> Handle(GetShipmentByIdQuery query, CancellationToken cancellationToken)
    {
        var shipment = await session.LoadAsync<Shipment>(query.Id, cancellationToken);

        if (shipment is null)
        {
            //throw new ReviewNotFoundException(query.Id);
        }

        return new GetShipmentByIdResult(shipment);
    }
}