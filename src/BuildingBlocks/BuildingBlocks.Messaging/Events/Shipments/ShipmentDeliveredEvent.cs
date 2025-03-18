using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events.Shipments;
public record ShipmentDeliveredEvent : IntegrationEvent
{
    public Guid ShipmentId { get; set; }
    public Guid OrderId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public Guid MerchantId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public DateTime OrderDeadline { get; set; }
}
