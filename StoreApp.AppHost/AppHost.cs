var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var ordersDb = postgres.AddDatabase("ordersdb");

var rabbitmq = builder.AddRabbitMQ("messaging")
                      .WithImage("rabbitmq", "4.0-management");

builder.AddProject<Projects.StoreApp_API>("api")
       .WithReference(rabbitmq)
       .WithReference(ordersDb) 
       .WithHttpEndpoint(port: 5000, targetPort: 8080);

builder.AddProject<Projects.StoreApp_Worker>("worker")
       .WithReference(rabbitmq)
       .WithReference(ordersDb); 

builder.Build().Run();