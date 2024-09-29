namespace Catalog.API.Product.CreateProduct;

using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid id);
internal class CreateProductCommandHandler : 
    ICommandHandler<CreateProductCommand, CreateProductResult>
{
    // Business logic to create a product
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create Product entity from command object
        // save to data
        // return CreateProductResult result

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO
        // save to database
        // return result

        return new CreateProductResult(Guid.NewGuid());
    }
}
