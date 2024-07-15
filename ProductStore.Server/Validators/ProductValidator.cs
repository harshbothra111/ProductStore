using FluentValidation;
using ProductStore.Application.DTOs;

namespace ProductStore.Server.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.");
            RuleFor(p => p.Quantity)
                .InclusiveBetween(1, 100).WithMessage("Quantity must be between 1 and 100.");
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Product code is required.");
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .PrecisionScale(20, 2, true).WithMessage("Price must have maximum 2 decimal places.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Category is required.");
            RuleFor(p => p.SubCategoryId)
                .GreaterThan(0).WithMessage("SubCategory is required.");
        }
    }
}
