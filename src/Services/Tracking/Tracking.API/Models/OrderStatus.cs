using Marten;

namespace Tracking.API.Models;

public class OrderStatus
{
    public Guid OrderId { get; set; }          // Unique identifier for the order
    public Guid TrackingId {get; set;}
    public string Courier {get;set;}
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow; 
    public string Status { get; set; } = "Pending"; // Default initialization 
    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;  // The timestamp when the update occurred


    // ShippingAddress
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string EmailAddress { get; set; } 
    public string ZipCode { get; set; }
}
