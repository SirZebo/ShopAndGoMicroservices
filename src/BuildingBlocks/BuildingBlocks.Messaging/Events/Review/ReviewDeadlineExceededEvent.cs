using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events.Review;
public record ReviewDeadlineExceededEvent : IntegrationEvent
{
    public Guid ReviewId { get; set; }
}
