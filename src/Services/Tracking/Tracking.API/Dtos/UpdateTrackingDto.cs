using Marten;

namespace Tracking.API.Dtos;

public class UpdateTrackingDto
{
    public string NewStatus { get; set; }
    public string FirstName {get;set;}
    public string LastName {get;set;}

    public string CustomerPhoneNumber{get;set;}

    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;  // The timestamp when the update occurred
}
