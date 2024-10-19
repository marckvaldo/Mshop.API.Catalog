using MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.Infra.Data.ES.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public bool IsSale { get; set; }

        public Guid CategoryId { get; set; }

        public string Category { get; set; }

        public string Thumb { get; private set; }


    public static ProductModel EntityToModel(Product Entity)
           => new ProductModel 
           { 
               Id = Entity.Id, 
               Name = Entity.Name, 
               Description = Entity.Description, 
               IsSale = Entity.IsSale, 
               Price = Entity.Price, 
               Stock = Entity.Stock, 
               CategoryId = Entity.CategoryId, 
               Thumb = Entity?.Thumb?.Path,
               Category = Entity.Category.Name
           };

        public static Product ModelToEntity(ProductModel Model)
        {
            var category = new Category(Model.Category, Model.CategoryId, true);
            var product = new Product(Model.Id, Model.Description, Model.Name, Model.Price, Model.Thumb, Model.Stock);
            product.AddCategory(category);
            return product;
        }

        public static List<Product> ListModelToListEntity(IEnumerable<ProductModel> productModels)
        {
            return productModels.Select(pm=> new Product(pm.Id,pm.Description,pm.Name,pm.Price,pm.Thumb,pm.Stock)).ToList();
        }

    }


}
