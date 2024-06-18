using FluentValidation.Results;
using MShop.Catalog.Domain.Validator;
using EntityDomain  = MShop.Core.DomainObject;


namespace MShop.Catalog.Domain.Entity
{
    public class Category : EntityDomain.Entity
    {

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        //Entity
        public List<Product> Products { get; private set; }

        public Category(string name,
            Guid id,
            bool isActive = true)
        {
            Name = name;
            IsActive = isActive;
            AddId(id);

        }

        public ValidationResult IsValid()
        {
            var categoryValidador = new CategoryValidator().Validate(this);
            return categoryValidador;
        }
    }
}
