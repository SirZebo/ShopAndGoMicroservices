namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public Guid Id { get; }
    public ProductNotFoundException(Guid Id) : base($"Product with id '{Id}' cannot be found!")
        => this.Id = Id;
}
