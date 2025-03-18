namespace Ordering.Domain.Enums;
public enum OrderStatus
{
    AwaitingPayment = 1,
    InProgress = 2,
    Shipping = 3,
    Delivered = 4,
    Cancelled = 5,
    Completed = 6,
}
