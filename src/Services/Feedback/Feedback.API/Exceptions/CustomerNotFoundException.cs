using BuildingBlocks.Exceptions;

namespace Feedback.API.Exceptions;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException(Guid Id) : base("Customer", Id)
    {
    }
}

