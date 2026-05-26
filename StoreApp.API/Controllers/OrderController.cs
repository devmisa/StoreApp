using Microsoft.AspNetCore.Mvc;
using StoreApp.API.Features.CreateOrder;

namespace StoreApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(ICreateOrderCommandHandler handler, CreateOrderValidator orderValidator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand orderDto)
        {
            var validationResult = await orderValidator.ValidateAsync(orderDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var result = await handler.HandleAsync(orderDto);
            return Accepted(result);
        }
    }
}
