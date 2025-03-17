using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Payments;
using MassTransit;
using Shipping.API.Dtos;
using Shipping.API.Enums;
using Shipping.API.Model;
using Shipping.API.Shipping.CreateShipment;

namespace Shipping.API.Shipping.EventHandler.Integration;

public class OrderUpdatedToInProgressEventHandler
    (ISender sender,
    ILogger<OrderUpdatedToInProgressEventHandler> logger)
    : IConsumer<OrderUpdatedToInProgressEvent>
{
    public async Task Consume(ConsumeContext<OrderUpdatedToInProgressEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = MapToReleasePaymentCommand(context.Message);
        await sender.Send(command);
    }

    private CreateShipmentCommand MapToReleasePaymentCommand(OrderUpdatedToInProgressEvent message)
    {
        var shippingAddressDto = new ShippingAddressDto
        {
            FirstName = message.FirstName,
            LastName = message.LastName,
            EmailAddress = message.EmailAddress,
            AddressLine = message.AddressLine,
            Country = message.Country,
            State = message.State,
            ZipCode = message.ZipCode,
        };

        var orderDto = new OrderDto
        {
            Id = message.OrderId,
            CustomerId = message.CustomerId,
            MerchantId = message.MerchantId,
            ProductId = message.ProductId,
            Quantity = message.Quantity,
            ShipmentDeadline = DateTime.UtcNow.Add(message.MaxCompletionTime),
            ShippingAddress = shippingAddressDto,
            Language = message.Language
        };

        var shipmentDto = new ShipmentDto
        {
            Order = orderDto,
        };

        return new CreateShipmentCommand
        (
           shipmentDto
        );
    }
}
