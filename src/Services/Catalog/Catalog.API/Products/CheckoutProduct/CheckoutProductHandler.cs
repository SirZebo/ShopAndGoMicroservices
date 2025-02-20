using BuildingBlocks.Messaging.Events;
using Catalog.API.Dtos;
using MassTransit;

namespace Catalog.API.Products.CheckoutProduct;

public record CheckoutProductCommand(CheckoutProductDto CheckoutProductDto)
    : ICommand<CheckoutProductResult>;

public record CheckoutProductResult(bool IsSuccess);

public class CheckoutProductCommandValidator
    : AbstractValidator<CheckoutProductCommand>
{
    public CheckoutProductCommandValidator()
    {
        RuleFor(x => x.CheckoutProductDto)
            .NotNull().WithMessage("CheckoutProductDto can't be null");
        RuleFor(x => x.CheckoutProductDto.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.CheckoutProductDto.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        // TODO
    }
}

public class CheckoutProductCommandHandler
    (IPublishEndpoint publishEndpoint, IDocumentSession session)
    : ICommandHandler<CheckoutProductCommand, CheckoutProductResult>
{
    public async Task<CheckoutProductResult> Handle(CheckoutProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.CheckoutProductDto.ProductId, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.CheckoutProductDto.ProductId);
        }

        var eventMessage = command.CheckoutProductDto.Adapt<ProductCheckoutEvent>();
        var totalPrice = CalculateTotalPrice(product, command.CheckoutProductDto.Quantity);

        eventMessage.Price = product.Price;
        eventMessage.TotalPrice = totalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CheckoutProductResult(true);
    }

    private decimal CalculateTotalPrice(Product product, int quantity)
    {
        return product.Price * quantity;
    }
}
