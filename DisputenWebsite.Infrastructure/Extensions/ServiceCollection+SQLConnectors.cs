using DisputenPWA.DAL.Repositories;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.Infrastructure.Extensions
{
    public static class ServiceCollectionSQLConnectorsExtension
    {
        public static IServiceCollection AddSQLConnectors(this IServiceCollection services)
        {
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IAppEventRepository, AppEventRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<IAttendeeRepository, AttendeeRepository>();

            services.AddTransient<IGroupConnector, GroupConnector>();
            services.AddTransient<IAppEventConnector, AppEventConnector>();
            services.AddTransient<IUserConnector, UserConnector>();
            services.AddTransient<IMemberConnector, MemberConnector>();
            services.AddTransient<IAttendeeConnector, AttendeeConnector>();

            services.AddTransient<ISeedingService, SeedingService>();
            return services;
        }
    }
}
