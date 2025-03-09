namespace BuildingBlocks.Messaging.Events;
public record CommercePayStartedEvent : IntegrationEvent
{
    public Guid PaymentId { get; set; }
    public Guid TransactionToken { get; set; }
    public decimal TransactionAmount { get; set; }
}
