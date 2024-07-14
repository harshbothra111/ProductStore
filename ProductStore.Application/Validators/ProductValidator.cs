using FluentValidation;
using ProductStore.Application.DTOs;

namespace ProductStore.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(2, 50).WithMessage("Product name must be between 2 and 50 characters.");
            RuleFor(p => p.Quantity)
                .InclusiveBetween(1, 100).WithMessage("Quantity must be between 1 and 100.");
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Product code is required.")
                .Length(2, 20).WithMessage("Product code must be between 2 and 20 characters.");
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Category is required.");
            RuleFor(p => p.SubCategoryId)
                .GreaterThan(0).WithMessage("SubCategory is required.");
        }
    }
}
