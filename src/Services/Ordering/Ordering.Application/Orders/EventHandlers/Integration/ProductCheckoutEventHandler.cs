﻿using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class ProductCheckoutEventHandler
    (ISender sender, ILogger<ProductCheckoutEventHandler> logger)
    : IConsumer<ProductCheckoutEvent>
{
    public async Task Consume(ConsumeContext<ProductCheckoutEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(ProductCheckoutEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            TransactionToken: message.TransactionToken,
            CustomerId: message.CustomerId,
            MerchantId: message.MerchantId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            Status: Ordering.Domain.Enums.OrderStatus.AwaitingPayment,
            MaxCompletionTime: message.MaxCompletionTime,
            TotalPrice: message.TotalPrice,
            OrderItems:
            [
                new OrderItemDto(orderId, message.ProductId, message.Quantity, message.Price),
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
