using FluentValidation;
using gymus_server.GymusApp.Store.Dtos.Requests;

namespace gymus_server.Shared.Validations.StoreValidations;

public class UpdateProductRequestValidation : AbstractValidator<ProductUpdateRequestDto> {
    public UpdateProductRequestValidation() {
        RuleFor(x => x.ProductName)
           .Length(3, 255)
           .WithMessage("product name should be between 3 to 255 characters")
           .When(x => !string.IsNullOrEmpty(x.ProductName));

        RuleFor(x => x.ProductDescription)
           .Length(3, 255)
           .WithMessage("product description should be between 3 to 255 characters")
           .When(x => !string.IsNullOrEmpty(x.ProductDescription));

        RuleFor(x => x.ProductImage)
           .NotEmpty()
           .WithMessage("product image is required")
           .When(x => x.ProductImage is not null);

        RuleFor(x => x.Quantity)
           .GreaterThanOrEqualTo(0)
           .WithMessage("quantity must be greater than or equal to 0")
           .When(x => !string.IsNullOrEmpty(x.Quantity.ToString()));

        RuleFor(x => x.Price)
           .GreaterThan(0)
           .WithMessage("product price must be greater than 0")
           .When(x => !string.IsNullOrEmpty(x.Price.ToString()));
    }
}