using Finance.API.Exceptions;

namespace Finance.API.Accounts.GetAccountById;

public record GetAccountByIdQuery(Guid Id) : IQuery<GetAccountByIdResult>;

public record GetAccountByIdResult(Account Account);
internal class GetAccountByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetAccountByIdQuery, GetAccountByIdResult>
{
    public async Task<GetAccountByIdResult> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        var account = await session.LoadAsync<Account>(query.Id, cancellationToken);

        if (account is null)
        {
            throw new AccountNotFoundException(query.Id);
        }

        return new GetAccountByIdResult(account);
    }
}