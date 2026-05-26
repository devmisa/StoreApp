using StoreApp.ServiceDefaults;
using StoreApp.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.AddRabbitMQClient("messaging");


var host = builder.Build();

host.Run();
