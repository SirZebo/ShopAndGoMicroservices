using Shipping.API.Enums;

namespace Shipping.API.Dtos;

public class ShipmentDto
{
    public Guid Id { get; set; } = default!;
    public string TrackingNumber { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public OrderDto Order { get; set; }
}
