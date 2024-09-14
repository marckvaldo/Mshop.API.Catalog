using FluentValidation.Results;
using MShop.Business.ValueObject;
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

        public bool IsSale { get; private set; }

        public Guid CategoryId { get; private set; }

        //Entity
        public Category Category { get; private set; }

        public FileImage? Thumb { get; private set; }

        public Product(Guid id,string description, string name, decimal price, string thumb, decimal stock = 0) : base()
        {
            AddId(id);
            Description = description;
            Name = name;
            Price = price;
            Stock = stock;
            //CategoryId = category.Id;
            //Category = category;
            Thumb = new FileImage(thumb);
            IsValid();
        }

        public ValidationResult IsValid()
        {
            var productValidador = new ProductValidator().Validate(this);
            return productValidador;           
        }

        public void AddCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
            IsValid();
        }

        public void UpdateThumb(string thumb)
        {
            Thumb = new FileImage(thumb);
            IsValid();
        }

    }
}
