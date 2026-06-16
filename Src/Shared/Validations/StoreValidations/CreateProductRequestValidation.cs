using FluentValidation;
using gymus_server.GymusApp.Store.Dtos.Requests;

namespace gymus_server.Shared.Validations.StoreValidations;

public class CreateProductRequestValidation : AbstractValidator<ProductCreateRequestDto> {
    public CreateProductRequestValidation() {
        RuleFor(x => x.ProductName)
           .NotNull()
           .WithMessage("product name is required")
           .NotEmpty()
           .WithMessage("product name is required")
           .Length(3, 255)
           .WithMessage("product name should be betweeb 3 to 255 characters");

        RuleFor(x => x.ProductDescription)
           .NotNull()
           .WithMessage("product description is required")
           .NotEmpty()
           .WithMessage("product description is required")
           .Length(3, 255);

        RuleFor(x => x.ProductImage)
           .NotNull()
           .WithMessage("product image is required")
           .NotEmpty()
           .WithMessage("product image is required");

        RuleFor(x => x.Quantity)
           .NotNull()
           .WithMessage("quantity is required")
           .NotEmpty()
           .WithMessage("quantity is required")
           .GreaterThan(0)
           .WithMessage("quantity must be greater than 0");

        RuleFor(x => x.Price)
           .NotNull()
           .WithMessage("product price is required")
           .NotEmpty()
           .WithMessage("product price is required")
           .GreaterThan(0)
           .WithMessage("product price must be greater than 0");
    }
}