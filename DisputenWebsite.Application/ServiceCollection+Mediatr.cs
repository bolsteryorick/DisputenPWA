using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DisputenPWA.Application
{
    public static class ServiceCollectionMediatrExtension
    {
        public static IServiceCollection AddApplicationMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionMediatrExtension).GetTypeInfo().Assembly);
            return services;
        }
    }
}
