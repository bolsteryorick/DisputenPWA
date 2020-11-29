using DisputenPWA.Application.Extensions.SubExtensions;
using DisputenPWA.Application.Members.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.Application.Extensions
{
    public static class ServiceCollectionBusinessLogicExtension
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddOperationAuthorizing();
            services.AddBusinessAuthorizing();
            services.AddTransient<ILeaveAllGroupEventsService, LeaveAllGroupEventsService>();
            return services;
        }
    }
}
