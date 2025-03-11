using Marten;

namespace Tracking.API.Dtos;

public class OrderStatusDto
{
    public string OrderId { get; set; }          // Unique identifier for the order
    public string TrackingId {get; set;}
    public string Courier {get;set;}
    public DateTime CreatedAt{get;set;}
    public string NewStatus { get; set; } = "Pending"; // Default initialization 
    public string CustomerName {get;set;}

    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;  // The timestamp when the update occurred
}
