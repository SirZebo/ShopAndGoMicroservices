var builder = WebApplication.CreateBuilder(args);

// Add Services to the container. - builder.AddServices()


var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
