using DisputenPWA.API.GraphQL.Mutations;
using DisputenPWA.API.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL
{
    public class DisputenAppSchema : Schema, ISchema
    {
        public DisputenAppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<DisputenAppQueries>();
            Mutation = resolver.Resolve<DisputenAppMutations>();
        }
    }
}
