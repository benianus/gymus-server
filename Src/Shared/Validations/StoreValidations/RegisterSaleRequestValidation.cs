using FluentValidation;
using gymus_server.GymusApp.Store.Dtos.Requests;

namespace gymus_server.Shared.Validations.StoreValidations;

public class RegisterSaleRequestValidation : AbstractValidator<SaleRegisterRequestDto> {
    public RegisterSaleRequestValidation() {
        RuleFor(x => x.Quantity)
           .NotEmpty()
           .WithMessage("quantity is required")
           .NotNull()
           .WithMessage("quantity is required")
           .GreaterThanOrEqualTo(0)
           .WithMessage("quantity must be greater than or equal to 0");

        RuleFor(x => x.TotalPrice)
           .NotEmpty()
           .WithMessage("total price is required")
           .NotNull()
           .WithMessage("total price is  required")
           .GreaterThanOrEqualTo(0)
           .WithMessage("quantity must be greater than or equal to 0");
    }
}