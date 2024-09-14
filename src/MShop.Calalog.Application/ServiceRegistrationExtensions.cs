using Microsoft.Extensions.DependencyInjection;
using MShop.Core.Message;
using System;
using System.Reflection;

namespace MShop.Calalog.Application
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ServiceRegistrationExtensions).Assembly));
            services.AddScoped<INotification, Notifications>();
            
            /*foreach (var service in services)
            {
                Console.WriteLine($"Service Type: {service.ServiceType}, Lifetime: {service.Lifetime}");
            }*/

            return services;
        }
    }
}
