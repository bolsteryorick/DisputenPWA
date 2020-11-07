using DisputenPWA.API.GraphQL;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace DisputenPWA.API.Extensions
{
    public static class ServiceCollectionConfigureGraphQLExtension
    {
        public static void AddGraphQLConfiguration(this IServiceCollection services)
        {

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>()
                .AddSingleton<IDocumentWriter, DocumentWriter>()
                .AddTransient<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
                .AddTransient<ISchema, DisputenAppSchema>()
                .AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }
    }
}
