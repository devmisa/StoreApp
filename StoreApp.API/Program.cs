using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using StoreApp.API.Features.CreateOrder;
using StoreApp.ServiceDefaults;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSingleton<ILogger>(logger);

builder.AddNpgsqlDbContext<OrdersDbContext>("ordersdb");
builder.AddRabbitMQClient("messaging");

builder.Services.AddScoped<ICreateOrderCommandHandler, CreateOrderCommandHandler>();
builder.Services.AddScoped<CreateOrderValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

    try
    {
        await dbContext.Database.EnsureCreatedAsync();
    }
    catch (Npgsql.PostgresException ex) when (ex.SqlState == "42P04")
    {
        var creator = dbContext.Database.GetService<IDatabaseCreator>() as IRelationalDatabaseCreator;
        if (creator != null && !await creator.HasTablesAsync())
        {
            await creator.CreateTablesAsync();
        }
    }
}

app.MapDefaultEndpoints();
app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();