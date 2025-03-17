using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Payments;
using MassTransit;
using Ordering.Application.Orders.Commands.UpdateOrder;
using Ordering.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.Application.Orders.Commands.UpdateOrderStatus;
public class UpdateOrderStatusHandler
    (IApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateOrderStatusCommand, UpdateOrderStatusResult>
{
    public async Task<UpdateOrderStatusResult> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        switch(order.Status)
        {
            case OrderStatus.InProgress:
                IntegrationEvent eventMessage = MapToOrderUpdatedToInProgressEvent(order);
                await publishEndpoint.Publish(eventMessage);
                break;
            case OrderStatus.Completed:
                eventMessage = new OrderCompletedEvent { OrderId = order.Id.Value };
                await publishEndpoint.Publish(eventMessage);
                break;
            case OrderStatus.Cancelled:
                eventMessage = new OrderCancelledEvent { OrderId = order.Id.Value };
                await publishEndpoint.Publish(eventMessage);
                break;
            default:
                throw new OrderStatusNotFoundException(command.Order.Id);
        }

        return new UpdateOrderStatusResult(true);

    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        order.Update(
            orderName: order.OrderName,
            shippingAddress: order.ShippingAddress,
            status: orderDto.Status
            );
    }

    private OrderUpdatedToInProgressEvent MapToOrderUpdatedToInProgressEvent(Order order)
    {
        return new OrderUpdatedToInProgressEvent
        {
            OrderId = order.Id.Value,
            CustomerId = order.CustomerId.Value,
            MerchantId = order.MerchantId,
            ProductId = order.OrderItems[0].Id.Value,
            Quantity = order.OrderItems[0].Quantity,
            MaxCompletionTime = order.MaxCompletionTime,
            OrderStatus = (int)order.Status,

            FirstName = order.ShippingAddress.FirstName,
            LastName = order.ShippingAddress.LastName,
            EmailAddress = order.ShippingAddress.EmailAddress,
            AddressLine = order.ShippingAddress.AddressLine,
            Country = order.ShippingAddress.Country,
            State = order.ShippingAddress.State,
            ZipCode = order.ShippingAddress.ZipCode
        };
    }

    
}