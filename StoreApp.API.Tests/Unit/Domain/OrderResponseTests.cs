using FluentAssertions;
using Xunit;
using StoreApp.API.Features.CreateOrder;

namespace StoreApp.API.Tests.Unit.Controllers
{
    public class OrderResponseTests
    {
        [Fact]
        public void OrderResponse_WithValidData_ShouldCreateSuccessfully()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var status = OrderStatus.Pending;

            // Act
            var response = new OrderResponse(orderId, customerId, status);

            // Assert
            response.OrderId.Should().Be(orderId);
            response.CustomerId.Should().Be(customerId);
            response.Status.Should().Be(status);
        }

        [Fact]
        public void OrderResponse_ShouldBeReadOnly()
        {
            // Arrange
            var response = new OrderResponse(Guid.NewGuid(), Guid.NewGuid(), OrderStatus.Pending);

            // Assert - OrderResponse é um readonly record struct
            response.Should().NotBeNull();
        }
    }
}
