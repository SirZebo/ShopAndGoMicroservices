using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container. - builder.AddServices()

// Automatically does DI of our handlers through assembly reflection
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);



builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<AccountInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

// Cross-Cutting Services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline automatically instead of DI in every time a new endpoint is created
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseCors("AllowAllOrigins");

app.Run();
