using DisputenPWA.Domain.SeedWorks.Cqrs;
using GraphQL.Types;

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
