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
                new Shipment()
                {
                    Id = new Guid("5805c332-ec79-4d6e-a48f-e685d10f0042"),
                    Order = GetPreconfiguredOrders().ToList()[1],
                    ShipmentStatus = Enums.ShipmentStatus.OrderReceived
                },
                new Shipment()
                {
                    Id = new Guid("5805c332-ec79-4d6e-a48f-e685d10f0042"),
                    Order = GetPreconfiguredOrders().ToList()[2],
                    ShipmentStatus = Enums.ShipmentStatus.OrderReceived
                },
                new Shipment()
                {
                    Id = new Guid("5805c332-ec79-4d6e-a48f-e685d10f0042"),
                    Order = GetPreconfiguredOrders().ToList()[3],
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
                new Order()
                {
                    Id = new Guid("3a34ff51-6106-46b4-84fb-de34e0938867"),
                    CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    MerchantId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                    ProductId = new Guid("3b423421-6dfb-4ec2-a54c-358bc19d30f1"),
                    Quantity = 1,
                    OrderDeadline = DateTime.UtcNow.AddDays(3),
                    Language = "Malay",
                    FirstName = "john", 
                    LastName = "doe", 
                    EmailAddress = "john@gmail.com", 
                    AddressLine = "Broadway No:1", 
                    Country = "England", 
                    State = "Nottingham", 
                    ZipCode = "08050"
                },
                new Order()
                {
                    Id = new Guid("19ac9cce-f730-41f9-8872-a75ef9c463fa"),
                    CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    MerchantId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                    ProductId = new Guid("3b423421-6dfb-4ec2-a54c-358bc19d30f1"),
                    Quantity = 1,
                    OrderDeadline = DateTime.UtcNow.AddDays(3),
                    Language = "Malay",
                    FirstName = "john",
                    LastName = "doe",
                    EmailAddress = "john@gmail.com",
                    AddressLine = "Broadway No:1",
                    Country = "England",
                    State = "Nottingham",
                    ZipCode = "08050"
                },
                new Order()
                {
                    Id = new Guid("5805c332-ec79-4d6e-a48f-e685d10f0042"),
                    CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa"),
                    MerchantId = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                    ProductId = new Guid("3b423421-6dfb-4ec2-a54c-358bc19d30f1"),
                    Quantity = 1,
                    OrderDeadline = DateTime.UtcNow.AddDays(3),
                    Language = "Malay",
                    FirstName = "john",
                    LastName = "doe",
                    EmailAddress = "john@gmail.com",
                    AddressLine = "Broadway No:1",
                    Country = "England",
                    State = "Nottingham",
                    ZipCode = "08050"
                },
                
            };
}
