using MShop.Catalog.Domain.Validator;
using MShop.Core.Exception;
using MShop.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Catalog.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; private set; }
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
            Id = id;
            Description = description;
            Name = name;
            Price = price;
            Stock = stock;
            IsActive = isActive;
            CategoryId = categoryId;
        }

        public void IsValid(INotification notification)
        {
            var productValidador = new ProductValidator(this, notification);
            productValidador.Validate();
            if (notification.HasErrors())
            {
                throw new EntityValidationException("Validation errors");
            }
        }

    }
}
