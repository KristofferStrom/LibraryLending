using LibraryLending.Application;
using LibraryLending.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();


app.Run();

