using DisputenPWA.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.Application.Extensions.SubExtensions
{
    public static class ServiceCollectionBusinessAuthorizingExtension
    {
        public static IServiceCollection AddBusinessAuthorizing(this IServiceCollection services)
        {
            services.AddTransient<IBusinessAuthorizer, BusinessAuthorizer>();
            return services;
        }
    }
}
