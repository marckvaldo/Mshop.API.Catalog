using MShop.Catalog.Domain.Entity;

namespace MShop.Catalog.Infra.Data.ES.Model
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public static CategoryModel EntityToModel(Category Entity) 
            =>  new CategoryModel { Id = Entity.Id, Name = Entity.Name, IsActive = Entity.IsActive };

        public static Category ModelToEntity(CategoryModel Model) 
            => new Category(Model.Name, Model.Id, Model.IsActive);

    }
}
