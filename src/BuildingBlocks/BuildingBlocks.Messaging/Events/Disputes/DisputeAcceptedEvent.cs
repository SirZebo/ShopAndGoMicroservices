

namespace BuildingBlocks.Messaging.Events.Disputes;
public record DisputeAcceptedEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
}
