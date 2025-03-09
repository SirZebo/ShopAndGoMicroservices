using BuildingBlocks.Exceptions;

namespace Finance.API.Exceptions;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(Guid Id) : base("Account", Id)
    {
    }
}