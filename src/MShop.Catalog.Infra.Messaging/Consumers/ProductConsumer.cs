using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MShop.Catalog.Infra.Messaging.Configuration;
using MShop.Catalog.Infra.Messaging.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


namespace MShop.Catalog.Infra.Messaging.Consumers
{
    public class ProductConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProductConsumer> _logger;
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;
        private readonly string _queue;
        private readonly IModel _channel;
       

        public ProductConsumer(
            IServiceProvider serviceProvider, 
            ILogger<ProductConsumer> logger,
            IModel channel, 
            IOptions<RabbitMQConfiguration> _rabbitMQConfiguration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _channel = channel; 
            _queue = _rabbitMQConfiguration.Value.QueueProducts;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageReceived;

             _channel.BasicConsume(
                queue: _queue, 
                autoAck: false, 
                consumer);

            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogWarning("Closing channel RabbitMQ");
            _channel.Dispose();
        }

        private void OnMessageReceived(
            object? sender, 
            BasicDeliverEventArgs eventArgs)
        {
            var scopedMediator = CreateNewScoped();

            string messageType = eventArgs.BasicProperties.Type ?? eventArgs.RoutingKey;

            var arrayMessage = eventArgs.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(arrayMessage);
            _logger.LogDebug(messageString);

           

            var input = new object();

            switch (messageType)
            {
                case "ProductCreatedEvent":
                case "ProductUpdatedEvent":
                    {
                        CreateCategory(messageString, scopedMediator).GetAwaiter().GetResult();
                        break;
                    }
                case "ProductRemovedEvent":
                    {
                        DeleteCategory(messageString, scopedMediator).GetAwaiter().GetResult();
                        break;
                    }
                default:
                    _logger.LogWarning("Unknown message type: {MessageType}", messageType);
                    break;
            }

            //await scopedMediator.Send(input, CancellationToken.None);
            _channel.BasicAck(eventArgs.DeliveryTag,false);
            
        }

        private IMediator CreateNewScoped()
        {
            //aqui eu crio um novo scop do asp.net 
            var scoped = _serviceProvider.CreateScope();
            //aqui eu recupero a instancia dos meus userCases
            return scoped.ServiceProvider.GetRequiredService<IMediator>();
        }

        private async Task<bool> CreateCategory(string messageString, IMediator mediator)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var message = JsonSerializer
                       .Deserialize<ProductMessage>(messageString);
            var input = ProductMessage.ProductMessageToCreateCategoryInput(message!);

            var result = await mediator.Send(input, CancellationToken.None);
            return result;  

        }

        private async Task<bool> DeleteCategory(string messageString, IMediator mediator)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var message = JsonSerializer
                       .Deserialize<ProductMessage>(messageString);
            var input = ProductMessage.ProductMessageToDeleteCategoryInput(message!.CategoryId);

            var result = await mediator.Send(input, CancellationToken.None);
            return result;
        }
    }
}
