namespace Finance.API.Accounts.CreateAccount;

public record CreateAccountCommand(string Name)
    : ICommand<CreateAccountResult>;

public record CreateAccountResult(Guid Id);

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
    }

}
internal class CreateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    // Business logic to create a product
    public async Task<CreateAccountResult> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Balance = 0
        };

        // save to database
        session.Store(account);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new CreateAccountResult(account.Id);
    }
}
