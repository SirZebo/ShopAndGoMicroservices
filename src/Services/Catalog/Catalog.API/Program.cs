var builder = WebApplication.CreateBuilder(args);

// Add Services to the container. - builder.AddServices()
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.Run();
