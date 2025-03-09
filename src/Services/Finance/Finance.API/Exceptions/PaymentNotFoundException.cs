using BuildingBlocks.Exceptions;

namespace Finance.API.Exceptions;

public class PaymentNotFoundException : NotFoundException
{
    public PaymentNotFoundException(Guid Id) : base("Payment", Id)
    {
    }
}