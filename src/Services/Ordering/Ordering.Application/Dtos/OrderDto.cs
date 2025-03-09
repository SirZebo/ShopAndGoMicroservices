using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos;
public record OrderDto(
    Guid Id,
    Guid CustomerId,
    Guid TransactionToken,
    string OrderName,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> OrderItems,
    TimeSpan MaxCompletionTime,
    decimal TotalPrice);