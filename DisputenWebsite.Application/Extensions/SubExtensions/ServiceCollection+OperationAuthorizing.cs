using DisputenPWA.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.Application.Extensions.SubExtensions
{
    public static class ServiceCollectionOperationAuthorizingExtension
    {
        public static IServiceCollection AddOperationAuthorizing(this IServiceCollection services)
        {
            services.AddTransient<IOperationAuthorizer, OperationAuthorizer>();
            return services;
        }
    }
}
