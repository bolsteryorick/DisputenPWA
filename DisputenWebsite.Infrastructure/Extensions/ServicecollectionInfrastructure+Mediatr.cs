using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DisputenPWA.Infrastructure.Extensions
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
