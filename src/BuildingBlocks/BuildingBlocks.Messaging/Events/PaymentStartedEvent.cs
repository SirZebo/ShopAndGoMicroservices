namespace BuildingBlocks.Messaging.Events;
public record PaymentStartedEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public decimal TotalPrice { get; set; }
}
