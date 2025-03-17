using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events.Payments;
public record OrderCancelledEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public decimal TotalPrice { get; set; }
}
