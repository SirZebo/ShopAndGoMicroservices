using BuildingBlocks.Messaging.Events.Shipments;
using MassTransit;
using Shipping.API.Dtos;
using Shipping.API.Model;

namespace Shipping.API.Shipping.CreateShipment;

public record CreateShipmentCommand(ShipmentDto Shipment)
    : ICommand<CreateShipmentResult>;

public record CreateShipmentResult(Guid Id);

internal class CreateShipmentCommandHandler
    (IDocumentSession session,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateShipmentCommand, CreateShipmentResult>
{
    public async Task<CreateShipmentResult> Handle(CreateShipmentCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = command.Shipment.Order.Id,
            CustomerId = command.Shipment.Order.CustomerId,
            MerchantId = command.Shipment.Order.MerchantId,
            ProductId = command.Shipment.Order.ProductId,
            Quantity = command.Shipment.Order.Quantity,
            OrderDeadline = command.Shipment.Order.OrderDeadline,
            Language = command.Shipment.Order.Language,

            FirstName = command.Shipment.Order.ShippingAddress.FirstName,
            LastName = command.Shipment.Order.ShippingAddress.LastName,
            EmailAddress = command.Shipment.Order.ShippingAddress.EmailAddress,
            AddressLine = command.Shipment.Order.ShippingAddress.AddressLine,
            Country = command.Shipment.Order.ShippingAddress.Country,
            State = command.Shipment.Order.ShippingAddress.State,
            ZipCode = command.Shipment.Order.ShippingAddress.ZipCode,
        };

        var shipment = new Shipment
        {
            Id = Guid.NewGuid(),
            ShipmentStatus = Enums.ShipmentStatus.OrderReceived,
            Order = order,
        };

        // save to database
        session.Store(shipment);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = new ShipmentCreatedEvent
        {
            ShipmentId = shipment.Id,
            OrderDeadline = shipment.Order.OrderDeadline
        };
        await publishEndpoint.Publish(eventMessage);

        // return result
        return new CreateShipmentResult(shipment.Id);
    }
}