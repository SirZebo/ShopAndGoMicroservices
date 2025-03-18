using BuildingBlocks.Messaging.Events.Review;
using BuildingBlocks.Messaging.Events.Shipments;
using MassTransit;
using Shipping.API.Exceptions;
using Shipping.API.Model;

namespace Shipping.API.Shipping.UpdateShipment;

public record UpdateShipmentCommand(Guid Id, string TrackingNumber) : ICommand<UpdateShipmentResult>;

public record UpdateShipmentResult(bool IsSuccess);

public class UpdateReviewValidator : AbstractValidator<UpdateShipmentCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Shipment Id is required");

        RuleFor(x => x.TrackingNumber)
            .NotEmpty().WithMessage("TrackingNumber is required")
            .Length(10, 20).WithMessage("TrackingNumber must be between 10 and 20 characters");
    }
}
internal class UpdateShipmentCommandHandler
    (IDocumentSession session,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<UpdateShipmentCommand, UpdateShipmentResult>
{
    public async Task<UpdateShipmentResult> Handle(UpdateShipmentCommand command, CancellationToken cancellationToken)
    {
        var shipment = await session.LoadAsync<Shipment>(command.Id, cancellationToken);

        if (shipment is null)
        {
            throw new ShipmentNotFoundException(command.Id);
        }

        if (shipment.ShipmentStatus != Enums.ShipmentStatus.OrderReceived)
        {
            throw new ShipmentAlreadySubmittedException(command.Id);
        }

        shipment.ShipmentStatus = Enums.ShipmentStatus.Delivered; // Temporary Hard Coded
        shipment.TrackingNumber = command.TrackingNumber;

        session.Update(shipment);
        await session.SaveChangesAsync(cancellationToken);

        if (shipment.ShipmentStatus == Enums.ShipmentStatus.Delivered)
        {
            var eventMessage = new ShipmentDeliveredEvent
            {
                OrderId = shipment.Order.Id,
                CustomerId = shipment.Order.CustomerId,
                MerchantId = shipment.Order.MerchantId,
                ProductId = shipment.Order.ProductId,
                Quantity = shipment.Order.Quantity,
                OrderDeadline = shipment.Order.OrderDeadline,
            };
            await publishEndpoint.Publish(eventMessage);
        }

        return new UpdateShipmentResult(true);
    }
}