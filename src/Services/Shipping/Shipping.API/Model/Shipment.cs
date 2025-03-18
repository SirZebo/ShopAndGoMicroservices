using Shipping.API.Enums;

namespace Shipping.API.Model;

public class Shipment
{
    public Guid Id { get; set; } = default!;
    public string TrackingNumber { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public Order Order { get; set; }

}