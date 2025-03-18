using BuildingBlocks.Exceptions;

namespace Finance.API.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid Id) : base("Order", Id)
    {
    }
}