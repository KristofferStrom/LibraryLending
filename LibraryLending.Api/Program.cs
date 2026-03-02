using LibraryLending.Application.DependencyInjection;
using LibraryLending.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeInfrastructureAsync();

app.MapOpenApi();

app.UseHttpsRedirection();


app.Run();

