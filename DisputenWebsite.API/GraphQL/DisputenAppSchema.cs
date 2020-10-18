using DisputenPWA.API.GraphQL.Mutations;
using DisputenPWA.API.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
