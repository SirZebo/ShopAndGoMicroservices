using Feedback.API.Model;
using Marten.Schema;

namespace Feedback.API.Data;

public class InitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Order>().AnyAsync())
            return;

        session.Store<Order>(GetPreconfiguredOrder());

        await session.SaveChangesAsync();

        session.Store<Review>(GetPreconfiguredReview());
        await session.SaveChangesAsync();
    }

    private static Order order1 = new Order()
    {
        Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
        CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
        MerchantId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
        ProductId = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
        Quantity = 1,
        MaxCompletionTime = TimeSpan.FromDays(3),
        OrderStatus = 4,
    };

    private static IEnumerable<Order> GetPreconfiguredOrder() => new List<Order>()
            {
                order1,
            };

    private static IEnumerable<Review> GetPreconfiguredReview() => new List<Review>()
            {

                new Review()
                {
                    Id = new Guid("2d517904-c141-47cf-9ba2-6eb5f015570f"),
                    Order = order1,
                    FeedbackStatus = Enums.FeedbackStatus.Incomplete,
                    DisputeStatus = Enums.DisputeStatus.NoDispute,
                    Body = ""
                },
            };
}
