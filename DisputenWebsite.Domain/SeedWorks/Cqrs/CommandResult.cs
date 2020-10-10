using DisputenPWA.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

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
