namespace StoreApp.API.Features.CreateOrder
{
    public record class CreateOrderCommand
    {
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
    }
}
