namespace BuildingBlocks.Messaging.Events.Payments;
public record OrderCompletedEvent : IntegrationEvent
{
    public Guid OrderId { get; set; } 
    public decimal TotalPrice { get; set; }
}
