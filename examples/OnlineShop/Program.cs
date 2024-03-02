using ClassLibrary1MagicHat.CQRS.Queries;
using MagicHat;
using MagicHat.CQRS.Commands;
using MagicHat.MongoDB;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CQRS.Commands;
using OnlineShop.CQRS.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMagicHat(builder.Configuration, cfg =>
{
    cfg.AddMongoDb(builder => {},  "MagicHat")
        .AddCommands()
        .AddQueries();
});

builder.Services.AddScoped<ICommandHandler<FirstCommand>, FirstCommandHandler>();
builder.Services.AddScoped<IQueryHandler<FirstQuery, FirstResultDto>, FirstQueryHandler>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet(
    "/commands/first",
    async ([FromServices] ICommandDispatcher commandDispatcher)
        => await commandDispatcher.DispatchAsync(new FirstCommand("Fake command value")));
app.MapGet(
    "/queries/first",
    async ([FromServices] IQueryDispatcher queryDispatcher)
        => await queryDispatcher.DispatchAsync(new FirstQuery("Fake query value")));

app.Run();
