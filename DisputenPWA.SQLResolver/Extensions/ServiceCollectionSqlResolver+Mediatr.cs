using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DisputenPWA.SQLResolver.Extensions
{
    public static class ServiceCollectionSqlResolverMediatrExtension
    {
        public static IServiceCollection AddInfratructureMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionSqlResolverMediatrExtension).GetTypeInfo().Assembly);
            return services;
        }
    }
}
