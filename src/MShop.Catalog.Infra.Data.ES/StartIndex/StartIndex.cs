using Microsoft.Extensions.DependencyInjection;
using MShop.Catalog.Infra.Data.ES.Model;
using Nest;
using System;

namespace MShop.Catalog.Infra.Data.ES.StartIndex
{
    public class StartIndex
    {
        public async Task CreateIndex(IServiceProvider serviceProvider)
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();

            await elastic.Indices.CreateAsync(IndexName.Category, i => i
            .Map<CategoryModel>(m => m
            .Properties(p => p

                .Keyword(k => k.
                    Name(category => category.Id))  
            
                 //text busca parcial
                .Text(t => t.Name(category => category.Name)
                    .Fields(f => f
                        //keyword busca exata
                        .Keyword(k => k
                            .Name(category => category.Name.Suffix("Keyword")))))
           
                .Boolean(b => b.
                    Name(category => category.IsActive))
            )));

            await elastic.Indices.CreateAsync(IndexName.Product, i => i
                .Map<ProductModel>(m => m
                    .Properties(p => p
                        .Keyword(k => k
                            .Name(product => product.Id)) // Assuming Id is a keyword field

                        .Text(t => t
                            .Name(product => product.Name)
                            .Fields(f => f
                                .Keyword(k => k
                                    .Name(product => product.Name.Suffix("keyword"))))) // Use "keyword" in lowercase for consistency

                        .Text(t => t
                            .Name(product => product.Description)
                            .Fields(f => f
                                .Keyword(k => k
                                    .Name(product => product.Description.Suffix("keyword"))))) // Use "keyword" in lowercase for consistency

                        .Number(n => n
                            .Name(product => product.Price)
                            .Type(NumberType.Float)) // Use NumberType.Float for float fields
                        .Number(n => n
                            .Name(product => product.Stock)
                            .Type(NumberType.Float)) // Use NumberType.Float for float fields
                        .Boolean(b => b
                            .Name(product => product.IsActive))
                        .Boolean(b => b
                            .Name(product => product.IsSale))
                        .Keyword(k => k
                            .Name(product => product.CategoryId))
                    )));
        }

        public void DeleteIndex(IServiceProvider serviceProvider)
        {
            var elastic = serviceProvider.GetRequiredService<IElasticClient>();
            elastic.Indices.Delete(IndexName.Category);
            elastic.Indices.Delete(IndexName.Product);
        }
    }
}
