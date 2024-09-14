using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MShop.Catalog.Infra.Messaging.Configuration;
using MShop.Catalog.Infra.Messaging.Consumers;
using RabbitMQ.Client;

namespace MShop.Catalog.Infra.Messaging
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<RabbitMQConfiguration>(
                configuration.GetSection(RabbitMQConfiguration.ConfigurationSection)
            );

            services.AddSingleton<IConnection>(
                sp =>
                {
                    RabbitMQConfiguration config = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;

                    var factory = new ConnectionFactory
                    {
                        HostName = config.HostName,
                        Port = config.Port,
                        UserName = config.UserName,
                        Password = config.Password,
                        VirtualHost = config.Vhost
                    };

                    return factory.CreateConnection();
                });

            return services;
        }
        
        public static IServiceCollection AddMenssageConsumer(this IServiceCollection services)
        {
            services.AddHostedService(
                sp =>
                {
                    var config = sp.GetRequiredService<IOptions<RabbitMQConfiguration>>();
                    var connection = sp.GetRequiredService<IConnection>();
                    var logger = sp.GetRequiredService<ILogger<ProductConsumer>>();
                    var channel = connection.CreateModel();

                    return new ProductConsumer(sp, 
                        logger,
                        channel,
                        config);
                });

            return services;
        }
       
       
    }
}
