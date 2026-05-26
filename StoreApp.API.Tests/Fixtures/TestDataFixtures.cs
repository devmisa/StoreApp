using Bogus;
using StoreApp.API.Features.CreateOrder;

namespace StoreApp.API.Tests.Fixtures
{
    public class TestDataFixtures
    {
        public static CreateOrderCommand GenerateValidCreateOrderCommand()
        {
            return new Faker<CreateOrderCommand>()
                .RuleFor(c => c.CustomerId, f => f.Random.Guid())
                .RuleFor(c => c.Total, f => f.Random.Decimal(1, 10000))
                .Generate();
        }

        public static CreateOrderCommand GenerateCreateOrderCommandWithCustomerId(Guid customerId)
        {
            return new Faker<CreateOrderCommand>()
                .RuleFor(c => c.CustomerId, _ => customerId)
                .RuleFor(c => c.Total, f => f.Random.Decimal(1, 10000))
                .Generate();
        }

        public static CreateOrderCommand GenerateCreateOrderCommandWithTotal(decimal total)
        {
            return new Faker<CreateOrderCommand>()
                .RuleFor(c => c.CustomerId, f => f.Random.Guid())
                .RuleFor(c => c.Total, _ => total)
                .Generate();
        }

        public static Order GenerateValidOrder()
        {
            return new Faker<Order>()
                .RuleFor(o => o.Id, f => f.Random.Guid())
                .RuleFor(o => o.CustomerId, f => f.Random.Guid())
                .RuleFor(o => o.Total, f => f.Random.Decimal(1, 10000))
                .RuleFor(o => o.Status, _ => OrderStatus.Pending)
                .RuleFor(o => o.CreatedAt, f => f.Date.PastDateOnly().ToDateTime(TimeOnly.MinValue))
                .Generate();
        }

        public static List<CreateOrderCommand> GenerateMultipleValidCreateOrderCommands(int count)
        {
            return new Faker<CreateOrderCommand>()
                .RuleFor(c => c.CustomerId, f => f.Random.Guid())
                .RuleFor(c => c.Total, f => f.Random.Decimal(1, 10000))
                .Generate(count);
        }
    }
}
