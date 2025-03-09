namespace Finance.API.Exceptions;

public class AccountInsufficientBalanceException : Exception
{
    public AccountInsufficientBalanceException(Guid Id) : base($"Account {Id} has insufficient balance")
    {
    }
}
