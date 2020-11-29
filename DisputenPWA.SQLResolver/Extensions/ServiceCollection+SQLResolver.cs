using DisputenPWA.SQLResolver.AppEvents;
using DisputenPWA.SQLResolver.Attendees;
using DisputenPWA.SQLResolver.Members;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.SQLResolver.Extensions
{
    public static class ServiceCollectionSQLResolverExtension
    {
        public static IServiceCollection AddSQLResolverServices(this IServiceCollection services)
        {
            services.AddTransient<IResolveForMembersService, ResolveForMembersService>();
            services.AddTransient<IResolveForAppEventsService, ResolveForAppEventsService>();
            services.AddTransient<IResolveForAttendeesService, ResolveForAttendeesService>();
            return services;
        }
    }
}
