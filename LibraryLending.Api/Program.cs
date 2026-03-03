using LibraryLending.Api.Extensions;
using LibraryLending.Application.DependencyInjection;
using LibraryLending.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

await app.Services.InitializeInfrastructureAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();