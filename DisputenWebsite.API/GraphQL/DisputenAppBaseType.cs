using DisputenPWA.Domain.SeedWorks.Cqrs;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL
{
    public class DisputenAppBaseType : ObjectGraphType
    {
        protected T ProcessResult<T>(ResolveFieldContext<object> context, ResultBase<T> result)
        {
            return result.Result;
        }
    }
}
