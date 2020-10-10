using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DisputenPWA.Application
{
    public static class ServiceCollectionMediatrExtension
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionMediatrExtension).GetTypeInfo().Assembly);
            return services;
        }
    }
}
