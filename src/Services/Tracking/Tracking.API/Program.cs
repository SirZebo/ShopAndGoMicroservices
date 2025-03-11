using HealthChecks.NpgSql;
using BuildingBlocks.Messaging.MassTransit;
using Tracking.API.Dtos;
using System.Net.Http;
using Tracking.API.Services;


// using Tracking.API.Services;


var builder = WebApplication.CreateBuilder(args);

// ✅ Register HttpClient for AfterShip
builder.Services.AddHttpClient<TrackingMoreService>();

// ✅ Add Authentication & Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Register Carter for API Routing
builder.Services.AddCarter();

// Register MediatR to enable handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<OrderStatusDto>().Identity(x => x.OrderId);
    opts.Schema.For<OrderStatus>().Identity(x => x.OrderId);

}).UseLightweightSessions();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Enable Carter routes
app.MapCarter();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}")
//     .WithStaticAssets();


app.Run();
