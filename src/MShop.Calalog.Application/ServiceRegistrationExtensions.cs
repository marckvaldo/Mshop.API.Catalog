using Microsoft.Extensions.DependencyInjection;
using MShop.Core.Message;

namespace MShop.Calalog.Application
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<INotification, Notifications>();
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ServiceRegistrationExtensions).Assembly));
            return services;
        }
    }
}
