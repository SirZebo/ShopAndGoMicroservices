namespace Shipping.API.Exceptions;

public class ShipmentAlreadySubmittedException : Exception
{
    public ShipmentAlreadySubmittedException(Guid Id) : base($"Shipment {Id} has already been submitted")
    {
    }
}