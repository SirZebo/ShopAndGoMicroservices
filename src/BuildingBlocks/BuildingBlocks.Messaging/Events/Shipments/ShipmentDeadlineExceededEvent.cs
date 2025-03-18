using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events.Shipments;
public record ShipmentDeadlineExceededEvent : IntegrationEvent
{
    public Guid ShipmentId { get; set; }
}

