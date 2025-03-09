using Marten.Schema;

namespace Finance.API.Data;

public class AccountInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Account>().AnyAsync())
            return;

        session.Store<Account>(GetPreconfiguredAccounts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Account> GetPreconfiguredAccounts() => new List<Account>()
            {
                new Account()
                {
                    Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
                    Name = "Darren",
                    Balance = 1000
                },
                new Account()
                {
                    Id = new Guid("db7a48da-79b0-4a29-ba03-00deb540baec"),
                    Name = "Ambrose",
                    Balance = 0
                },

            };
}