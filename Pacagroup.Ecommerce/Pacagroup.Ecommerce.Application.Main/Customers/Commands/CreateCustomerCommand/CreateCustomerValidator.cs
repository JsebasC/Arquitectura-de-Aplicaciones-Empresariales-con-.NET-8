using FluentValidation;

namespace Pacagroup.Ecommerce.Application.Features.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull().MinimumLength(5);
        }
    }
}
