using MShop.Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Products.Commun
{
    public class ProductOutPut
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
        public ProductOutPut(Guid id, string description, string name, decimal price, decimal stock, bool isSale, Guid categoryId, string category, string thumb)
        {
            Id = id;
            Description = description;
            Name = name;
            Price = price;
            Stock = stock;
            IsSale = isSale;
            CategoryId = categoryId;
            Category = category;
            Thumb = thumb;
        }

        public static ProductOutPut Error() 
            => new ProductOutPut(Guid.Empty, string.Empty, string.Empty, 0, 0, false, Guid.Empty, string.Empty, string.Empty);

        public static ProductOutPut FromEntity(Product entity)
            => new ProductOutPut(
                entity.Id,
                entity.Description,
                entity.Name,
                entity.Price,
                entity.Stock,
                entity.IsSale,
                entity.CategoryId,
                entity.Category.Name,
                entity.Thumb.Path);
    }
}
