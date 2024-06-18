using FluentValidation;
using MShop.Catalog.Domain.Entity;
using MShop.Core.DomainObject;
using MShop.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Catalog.Domain.Validator
{
    public class CategoryValidator : AbstractValidator<Category>
    {
      
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Name must be at most 30 characters long.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(category => category.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");
        }
    }
}
