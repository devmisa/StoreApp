namespace StoreApp.API.Features.CreateOrder
{
    public interface ICreateOrderCommandHandler
    {
        Task<OrderResponse> HandleAsync(CreateOrderCommand command);
    }
}
