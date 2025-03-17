namespace Tracking.API.Models;


public class Meta
{
    public int Code { get; set; }
    public string Message { get; set; }
}

public class OriginInfo
{
    public string CourierCode { get; set; }
    public string CourierPhone { get; set; }
    public string Weblink { get; set; }
    public string TrackingLink { get; set; }
    public object ReferenceNumber { get; set; }
    public MilestoneDate MilestoneDate { get; set; }
    public object PickupDate { get; set; }
    public object DepartedAirportDate { get; set; }
    public object ArrivedAbroadDate { get; set; }
    public object CustomsReceivedDate { get; set; }
    public List<object> TrackInfo { get; set; }
}

public class DestinationInfo
{
    public object CourierCode { get; set; }
    public object CourierPhone { get; set; }
    public object Weblink { get; set; }
    public object TrackingLink { get; set; }
    public object ReferenceNumber { get; set; }
    public MilestoneDate MilestoneDate { get; set; }
    public object PickupDate { get; set; }
    public object DepartedAirportDate { get; set; }
    public object ArrivedAbroadDate { get; set; }
    public object CustomsReceivedDate { get; set; }
    public List<object> TrackInfo { get; set; }
}

public class MilestoneDate
{
    public object InforeceivedDate { get; set; }
    public object PickupDate { get; set; }
    public object OutForDeliveryDate { get; set; }
    public object DeliveryDate { get; set; }
    public object ReturningDate { get; set; }
    public object ReturnedDate { get; set; }
}

public class TrackingData
{
    public string Id { get; set; }
    public string TrackingNumber { get; set; }
    public string CourierCode { get; set; }
    public string OrderNumber { get; set; }
    public string DeliveryStatus { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string RecipientPostcode { get; set; }
    public string OrderId { get; set; }
    public OriginInfo OriginInfo { get; set; }
    public DestinationInfo DestinationInfo { get; set; }
}

public class TrackingResponse
{
    public Meta Meta { get; set; }
    public List<TrackingData> Data { get; set; }
}
