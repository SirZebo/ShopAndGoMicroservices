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
        OrderDeadline = DateTime.UtcNow.AddDays(3)
    };

    private static Order order2 = new Order()
    {
        Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
        CustomerId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
        MerchantId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
        ProductId = new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
        Quantity = 1,
        OrderDeadline = DateTime.UtcNow.AddDays(5)
    };

    private static Order order3 = new Order()
    {
        Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
        CustomerId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
        MerchantId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
        ProductId = new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
        Quantity = 1,
        OrderDeadline = DateTime.UtcNow.AddDays(7)
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
                    Body = "",
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                },
                new Review()
                {
                    Id = new Guid("990f33c6-531e-4069-a08b-e42791456646"),
                    Order = order2,
                    FeedbackStatus = Enums.FeedbackStatus.Incomplete,
                    DisputeStatus = Enums.DisputeStatus.NoDispute,
                    Body = "",
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                },
                new Review()
                {
                    Id = new Guid("e10522b3-e22e-4a50-9576-a9f4ee1d4627"),
                    Order = order3,
                    FeedbackStatus = Enums.FeedbackStatus.Incomplete,
                    DisputeStatus = Enums.DisputeStatus.NoDispute,
                    Body = "",
                    CreatedAt = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                },
            };
}
