using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events;
public record PaymentDeletedEvent : IntegrationEvent
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
}
