using Npgsql;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text.Json;
using ILogger = Serilog.ILogger;

namespace StoreApp.API.Features.CreateOrder
{
    public class CreateOrderCommandHandler(ILogger logger,
        IConnection rabbitConn,
        OrdersDbContext context) : ICreateOrderCommandHandler
    {
        public async Task<OrderResponse> HandleAsync(CreateOrderCommand command)
        {
            var entity = Order.ToEntity(command);

            try
            {
                await context.Orders.AddAsync(entity);
                await context.SaveChangesAsync();
           
                await PublishToQueue(entity.Id, entity.CustomerId, entity.Status);
            }
            catch (NpgsqlException ex)
            {
                logger.Error(
                    ex,
                    "Ocorreu um erro ao criar o pedido para o cliente: {CustomerId}",
                    command.CustomerId);
            }
            catch (BrokerUnreachableException ex)
            {
                logger.Error(
                    ex,
                    "Não foi possível conectar ao RabbitMQ.");
            }

            return new OrderResponse(entity.Id, entity.CustomerId, entity.Status);
        }

        private async Task PublishToQueue(Guid orderId, Guid customerId, OrderStatus status)
        {
            using var channel = await rabbitConn.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                 queue: "order-processing",
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null
             );

            var payload = new { OrderId = orderId, CustomerId = customerId, Status = status };
            var body = JsonSerializer.SerializeToUtf8Bytes(payload);

            var props = new BasicProperties();

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "order-processing",
                mandatory: false,
                basicProperties: props,
                body: body
            );
        }
    }

    public readonly record struct OrderResponse(
        Guid OrderId,
        Guid CustomerId,
        OrderStatus Status);
}
