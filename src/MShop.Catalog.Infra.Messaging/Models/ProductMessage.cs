using MShop.Calalog.Application.UseCases.Category.CreateCategory;
using MShop.Calalog.Application.UseCases.Category.DeleteCategory;

namespace MShop.Catalog.Infra.Messaging.Models
{
    public class ProductMessage
    {
        public Guid ProductId { get;  set; }

        public string Description { get; set; }

        public string Name { get;  set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public bool IsActive { get;  set; }

        public Guid CategoryId { get; set; }

        public string Category { get; set; }

        public string? Thumb { get; set; }

        public bool IsSale { get; set; }

        //public DateTime OccuredOn { get; set; }

        public static CreateCategoryInput ProductMessageToCreateCategoryInput(ProductMessage product)
        {
            return new CreateCategoryInput(product.CategoryId, product.Category, true);
        }

        public static DeleteCategoryInput ProductMessageToDeleteCategoryInput(Guid id)
        {
            return new DeleteCategoryInput(id);
        }

        //aqui products imput
    }
}
