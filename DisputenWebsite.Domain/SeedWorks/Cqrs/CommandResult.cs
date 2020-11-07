using DisputenPWA.Domain.Errors;

namespace DisputenPWA.Domain.SeedWorks.Cqrs
{
    public class CommandResult<T> : ResultBase<T>
    {
        protected CommandResult(T result) : base(result)
        {
        }

        protected CommandResult(Error error) : base(error)
        {

        }
    }
}
