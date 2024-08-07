﻿using MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.Infra.Data.ES.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public bool IsActive { get; set; }

        public bool IsSale { get; set; }

        public Guid CategoryId { get; set; }


        public static ProductModel EntityToModel(Product Entity)
           => new ProductModel { Id = Entity.Id, Name = Entity.Name, IsActive = Entity.IsActive };

        public static Product ModelToEntity(ProductModel Model)
            => new Product(Model.Id, Model.Description, Model.Name, Model.Price, Model.CategoryId, Model.Stock, Model.IsActive);

        public static List<Product> ListModelToListEntity(IEnumerable<ProductModel> productModels)
        {
            return productModels.Select(pm=> new Product(pm.Id,pm.Description,pm.Name,pm.Price,pm.CategoryId,pm.Stock,pm.IsActive)).ToList();
        }

    }


}
