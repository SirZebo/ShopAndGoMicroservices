using BuildingBlocks.Exceptions;

namespace Shipping.API.Exceptions;

public class MerchantNotFoundException : NotFoundException
{
    public MerchantNotFoundException(Guid Id) : base("Merchant", Id)
    {
    }
}