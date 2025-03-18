using Marten.Schema;
using Shipping.API.Model;

namespace Shipping.API.Data;

public class InitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Shipment>().AnyAsync())
            return;
        session.Store<Order>(GetPreconfiguredOrders());
        session.Store<Shipment>(GetPreconfiguredShipments());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Shipment> GetPreconfiguredShipments() => new List<Shipment>()
            {
                new Shipment()
                {
                    Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
                    Order = GetPreconfiguredOrders().ToList()[0],
                    ShipmentStatus = Enums.ShipmentStatus.OrderReceived
                },
                

            };

    private static IEnumerable<Order> GetPreconfiguredOrders() => new List<Order>()
            {
                new Order()
                {
                    Id = new Guid("14534836-bdbe-4dbe-af1c-80f9d5f433c2"),
                    CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    MerchantId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                    ProductId = new Guid("3b423421-6dfb-4ec2-a54c-358bc19d30f1"),
                    Quantity = 1,
                    OrderDeadline = DateTime.UtcNow.AddDays(3),
                    Language = "Malay",
                    FirstName = "mehmet", 
                    LastName = "ozkaya", 
                    EmailAddress = "mehmet@gmail.com", 
                    AddressLine = "Bahcelievler No:4", 
                    Country = "Turkey", 
                    State = "Istanbul", 
                    ZipCode = "38050"
                },
            };
}
