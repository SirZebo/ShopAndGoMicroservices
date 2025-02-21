namespace BuildingBlocks.Messaging.Events;
public record PaymentStartedEvent : IntegrationEvent
{
    public Guid PaymentId { get; set; }
}
