using MShop.Catalog.Domain.Validator;
using MShop.Core.Exception;

namespace MShop.Catalog.Domain.Entity
{
    public class Category
    {
        public Guid Id { get; private set; }
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
            Id = id;

        }

        public void IsValid(Core.Message.INotification notification)
        {
            var categoryValidador = new CategoryValidator(this, notification);
            categoryValidador.Validate();
            if (notification.HasErrors())
            {
                throw new EntityValidationException("Validation errors");
            }

        }

    }
}
