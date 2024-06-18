using FluentValidation.Results;
using MShop.Catalog.Domain.Validator;
using EntityDomain = MShop.Core.DomainObject;

namespace MShop.Catalog.Domain.Entity
{
    public class Product: EntityDomain.Entity
    {
        public string Description { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Stock { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsSale { get; private set; }

        public Guid CategoryId { get; private set; }

        //public Dimensions Dimensions { get; private set; }

        //Entity
        public Category Category { get; private set; }

        //public FileImage? Thumb { get; private set; }

        public Product(Guid id,string description, string name, decimal price, Guid categoryId, decimal stock = 0, bool isActive = false) : base()
        {
            AddId(id);
            Description = description;
            Name = name;
            Price = price;
            Stock = stock;
            IsActive = isActive;
            CategoryId = categoryId;
        }

        public ValidationResult IsValid()
        {
            var productValidador = new ProductValidator().Validate(this);
            return productValidador;           
        }

    }
}
