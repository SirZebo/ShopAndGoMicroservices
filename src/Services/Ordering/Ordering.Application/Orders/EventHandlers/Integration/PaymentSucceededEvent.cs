using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.UpdateOrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration;
public class PaymentSucceededEventHandler
    (ISender sender, ILogger<PaymentSucceededEventHandler> logger)
    : IConsumer<PaymentSucceededEvent>
{
    public async Task Consume(ConsumeContext<PaymentSucceededEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToUpdateOrderStatusCommand(context.Message);
        await sender.Send(command);
    }

    private UpdateOrderStatusCommand MapToUpdateOrderStatusCommand(PaymentSucceededEvent message)
    {
        var orderDto = new OrderDto
            (
                Id: message.Id,
                CustomerId: Guid.Empty,
                TransactionToken: Guid.Empty,
                OrderName: null,
                ShippingAddress: null,
                BillingAddress: null,
                Payment: null,
                OrderItems: null,
                MaxCompletionTime: TimeSpan.Zero,
                Status: Ordering.Domain.Enums.OrderStatus.InProgress,
                TotalPrice: 0
            );
        return new UpdateOrderStatusCommand(orderDto);
    }
}

