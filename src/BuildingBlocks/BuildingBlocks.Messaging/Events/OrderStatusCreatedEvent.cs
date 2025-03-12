namespace BuildingBlocks.Messaging.Events;

public record OrderStatusCreatedEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }= default!;          // Unique identifier for the order
    public Guid TrackingId {get; set;} = default!; 
    public string Courier {get;set;} = default!; 
    public string CreatedAt{get;set;} = default!; 
    public string NewStatus { get; set; } = default!;
    public DateTime UpdateTime { get; set; } = default!;
}
