namespace BuildingBlocks.Messaging.Events;
public record PaymentCreatedEvent : IntegrationEvent
{
    public Guid PaymentId { get; set; }
    public Guid TransactionToken { get; set; }
    public decimal TotalPrice { get; set; }
}
