using BuildingBlocks.Messaging.Events;

public record ShipmentDeliveredEvent : IntegrationEvent
{
    public Guid OrderId { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public Guid MerchantId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public TimeSpan MaxCompletionTime { get; set; }
    public int OrderStatus { get; set; } = default!;
}

