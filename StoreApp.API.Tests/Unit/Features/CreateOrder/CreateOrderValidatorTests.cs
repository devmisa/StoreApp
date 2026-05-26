using FluentAssertions;
using Xunit;
using StoreApp.API.Features.CreateOrder;
using StoreApp.API.Tests.Fixtures;

namespace StoreApp.API.Tests.Unit.Features.CreateOrder
{
    public class CreateOrderValidatorTests
    {
        private readonly CreateOrderValidator _validator;

        public CreateOrderValidatorTests()
        {
            _validator = new CreateOrderValidator();
        }

        [Fact]
        public async Task Validate_WithValidCommand_ShouldReturnSuccess()
        {
            // Arrange
            var command = TestDataFixtures.GenerateValidCreateOrderCommand();

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task Validate_WithEmptyCustomerId_ShouldReturnError()
        {
            // Arrange
            var command = TestDataFixtures.GenerateCreateOrderCommandWithCustomerId(Guid.Empty);

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors[0].PropertyName.Should().Be("CustomerId");
            result.Errors[0].ErrorMessage.Should().Contain("obrigatório");
        }

        [Fact]
        public async Task Validate_WithZeroTotal_ShouldReturnError()
        {
            // Arrange
            var command = TestDataFixtures.GenerateCreateOrderCommandWithTotal(0m);

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors[0].PropertyName.Should().Be("Total");
            result.Errors[0].ErrorMessage.Should().Contain("maior que zero");
        }

        [Fact]
        public async Task Validate_WithNegativeTotal_ShouldReturnError()
        {
            // Arrange
            var command = TestDataFixtures.GenerateCreateOrderCommandWithTotal(-100m);

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle();
            result.Errors[0].PropertyName.Should().Be("Total");
        }

        [Fact]
        public async Task Validate_WithMultipleErrors_ShouldReturnAllErrors()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = Guid.Empty,
                Total = -50m
            };

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(e => e.PropertyName == "CustomerId");
            result.Errors.Should().Contain(e => e.PropertyName == "Total");
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(1)]
        [InlineData(100.50)]
        [InlineData(10000)]
        public async Task Validate_WithValidTotals_ShouldReturnSuccess(decimal total)
        {
            // Arrange
            var command = TestDataFixtures.GenerateCreateOrderCommandWithTotal(total);

            // Act
            var result = await _validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
