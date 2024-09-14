using MediatR;
using MShop.Calalog.Application;
using MShop.Catalog.API.GraphQL.Category;
using MShop.Catalog.Infra.Data.ES;
using MShop.Catalog.Infra.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddUseCases()
    .AddElasticSearch(builder.Configuration)
    .AddRepository()
    .AddRabbitMQ(builder.Configuration)
    .AddMenssageConsumer()
    .AddGraphQLServer()
    .AddQueryType()
    .AddMutationType()
    .AddTypeExtension<CategoryQuery>()
    .AddTypeExtension<CategoryMutations>();
    

//services.AddScoped<IMediator>(provider => provider.GetRequiredService<IMediator>());


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();
