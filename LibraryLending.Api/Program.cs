using LibraryLending.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();


app.Run();

