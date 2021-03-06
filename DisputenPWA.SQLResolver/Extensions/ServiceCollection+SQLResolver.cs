using DisputenPWA.SQLResolver.AppEvents;
using DisputenPWA.SQLResolver.Attendees;
using DisputenPWA.SQLResolver.Contacts;
using DisputenPWA.SQLResolver.Groups;
using DisputenPWA.SQLResolver.Members;
using DisputenPWA.SQLResolver.Users;
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
            services.AddTransient<IResolveForGroupsService, ResolveForGroupsService>();
            services.AddTransient<IResolveForUserService, ResolveForUserService>();
            services.AddTransient<IResolveForContactsService, ResolveForContactsService>();
            return services;
        }
    }
}
