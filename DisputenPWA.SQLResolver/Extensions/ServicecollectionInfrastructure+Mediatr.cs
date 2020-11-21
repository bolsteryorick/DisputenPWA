using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DisputenPWA.SQLResolver.Extensions
{
    public static class ServicecollectionInfrastructureMediatrExtension
    {
        public static IServiceCollection AddInfratructureMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServicecollectionInfrastructureMediatrExtension).GetTypeInfo().Assembly);
            return services;
        }
    }
}
