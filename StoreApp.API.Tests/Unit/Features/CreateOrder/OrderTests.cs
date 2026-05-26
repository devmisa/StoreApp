using FluentAssertions;
using Xunit;
using StoreApp.API.Features.CreateOrder;
using StoreApp.API.Tests.Fixtures;

namespace StoreApp.API.Tests.Unit.Features.CreateOrder
{
    public class OrderTests
    {
        [Fact]
        public void ToEntity_WithValidCommand_ShouldMapCorrectly()
        {
            // Arrange
            var command = TestDataFixtures.GenerateValidCreateOrderCommand();

            // Act
            var order = Order.ToEntity(command);

            // Assert
            order.Should().NotBeNull();
            order.CustomerId.Should().Be(command.CustomerId);
            order.Total.Should().Be(command.Total);
            order.Status.Should().Be(OrderStatus.Pending);
            order.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            order.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void ToEntity_ShouldGenerateUniqueIds()
        {
            // Arrange
            var command1 = TestDataFixtures.GenerateValidCreateOrderCommand();
            var command2 = TestDataFixtures.GenerateValidCreateOrderCommand();

            // Act
            var order1 = Order.ToEntity(command1);
            var order2 = Order.ToEntity(command2);

            // Assert
            order1.Id.Should().NotBe(order2.Id);
        }

        [Fact]
        public void ToEntity_ShouldSetStatusToPending()
        {
            // Arrange
            var command = TestDataFixtures.GenerateValidCreateOrderCommand();

            // Act
            var order = Order.ToEntity(command);

            // Assert
            order.Status.Should().Be(OrderStatus.Pending);
        }

        [Fact]
        public void ToEntity_WithDifferentTotals_ShouldMaintainTotalValue()
        {
            // Arrange
            var total = 999.99m;
            var command = TestDataFixtures.GenerateCreateOrderCommandWithTotal(total);

            // Act
            var order = Order.ToEntity(command);

            // Assert
            order.Total.Should().Be(total);
        }

        [Fact]
        public void ToEntity_WithSpecificCustomerId_ShouldMaintainCustomerId()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var command = TestDataFixtures.GenerateCreateOrderCommandWithCustomerId(customerId);

            // Act
            var order = Order.ToEntity(command);

            // Assert
            order.CustomerId.Should().Be(customerId);
        }
    }
}
