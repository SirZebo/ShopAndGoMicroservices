using Marten;

namespace Tracking.API.Dtos;

public class UpdateTrackingDto
{
    public string NewStatus { get; set; } = "Pending"; // Default initialization 
    public string CustomerName {get;set;}

    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;  // The timestamp when the update occurred
}
