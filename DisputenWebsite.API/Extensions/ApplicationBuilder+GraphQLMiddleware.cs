using DisputenPWA.API.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DisputenPWA.API.Extensions
{
    public static class ApplicationBuilderGraphQLMiddlewareExtension
    {
        public static void UseGraphQL(this IApplicationBuilder app)
        {
            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings());
        }

        public class GraphQLSettings
        {
            public PathString Path { get; set; } = "/api/graphql";
        }
    }
}
