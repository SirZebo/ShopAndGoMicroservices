using Marten;

namespace Tracking.API.Models;

public class OrderStatus
{
    public Guid OrderId { get; set; }          // Unique identifier for the order
    public Guid TrackingId {get; set;}
    public string Courier {get;set;}
    public DateTime CreatedAt{get;set;}
    public string Status { get; set; } = "Pending"; // Default initialization 
    public string Name {get;set;}
    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;  // The timestamp when the update occurred
}
