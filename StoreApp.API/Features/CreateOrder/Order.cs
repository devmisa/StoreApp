namespace StoreApp.API.Features.CreateOrder
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }

        public static Order ToEntity(CreateOrderCommand command) => new()
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            Total = command.Total,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

    }
}
