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
        //session.Store<Account>(new Account()
        //{
        //    Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
        //    Name = "Darren",
        //    Balance = 1000
        //});

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
                new Account()
                {
                    Id = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    Name = "mehmet",
                    Balance = 0
                },
                new Account()
                {
                    Id = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                    Name = "john",
                    Balance = 0
                },

            };
}