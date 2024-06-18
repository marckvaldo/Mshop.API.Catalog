using FluentValidation;
using MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.Domain.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // Description
            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .MinimumLength(10).WithMessage("{PropertyName} should be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("{PropertyName} should be at most 1000 characters long.");

            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .MinimumLength(3).WithMessage("{PropertyName} should be at least 3 characters long.")
                .MaximumLength(255).WithMessage("{PropertyName} should be at most 255 characters long.");

            // Price
            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("{PropertyName} should be a positive number.")
                .GreaterThanOrEqualTo(0.1M).WithMessage("{PropertyName} should be at least 0.1.");

            RuleFor(category => category.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");
        }

    }
}
