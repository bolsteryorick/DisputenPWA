using DisputenPWA.Domain.Errors;

namespace DisputenPWA.Domain.SeedWorks.Cqrs
{
    public class QueryResult<T> : ResultBase<T>
    {
        protected QueryResult(T result) : base(result)
        {
        }

        protected QueryResult(Error error) : base(error)
        {

        }
    }
}
