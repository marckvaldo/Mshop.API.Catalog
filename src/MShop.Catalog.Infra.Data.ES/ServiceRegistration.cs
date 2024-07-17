
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShop.Catalog.Domain.Respositories;
using MShop.Catalog.Infra.Data.ES.Model;
using MShop.Catalog.Infra.Data.ES.Repositoty;
using Nest;


namespace MShop.Catalog.Infra.Data.ES
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ElasticSearch");
            var userElastic = configuration.GetConnectionString("Username");
            var passwordElastic = configuration.GetConnectionString("Password");

            var uri = new Uri(connectionString);


            var settings = new ConnectionSettings(uri)
                .BasicAuthentication(userElastic,passwordElastic)

                //aqui estou falando que toda operação que tenha a instancia de categoryModel ele salve no index que esta em StartIndex.IndexName.Category
                //estou tambem forçando que a propriedade Id do documento do elastic vai ser o conteudo Id da instacia de categoryModel 
                .DefaultMappingFor<CategoryModel>(i=>i
                    .IndexName(StartIndex.IndexName.Category)
                    .IdProperty(p=>p.Id))
                .DefaultMappingFor<ProductModel>(i=>i
                    .IndexName(StartIndex.IndexName.Product)
                    .IdProperty(p=>p.Id))
                //.EnableDebugMode()
                .PrettyJson()
                .ThrowExceptions()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(settings);
            service.AddSingleton<IElasticClient>(client);
            return service;
        }

        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            return service;
        }
    }
}
