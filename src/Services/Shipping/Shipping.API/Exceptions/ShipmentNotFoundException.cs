using BuildingBlocks.Exceptions;

namespace Shipping.API.Exceptions;

public class ShipmentNotFoundException : NotFoundException
{
    public ShipmentNotFoundException(Guid Id) : base("Shipment", Id)
    {
    }
}