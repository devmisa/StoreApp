using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace StoreApp.Worker;

public class Worker(ILogger<Worker> logger, IConnection rabbitConnection) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        using var channel = await rabbitConnection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "order-processing", durable: true, exclusive: false, autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            logger.LogInformation("Concluído: Processando pedido recebido do Aspire AppHost: {Message}", message);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(queue: "order-processing", autoAck: false, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
