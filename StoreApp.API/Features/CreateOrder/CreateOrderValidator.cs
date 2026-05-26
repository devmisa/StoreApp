using FluentValidation;

namespace StoreApp.API.Features.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId é obrigatório.");
            RuleFor(x => x.Total).GreaterThan(0).WithMessage("Total deve ser maior que zero.");
        }
    }
}
