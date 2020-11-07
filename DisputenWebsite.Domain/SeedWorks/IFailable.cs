using DisputenPWA.Domain.Errors;
using System.Collections.Generic;

namespace DisputenPWA.Domain.SeedWorks
{
    public interface IFailable
    {
        IEnumerable<Error> Errors { get; }
        void AddError(Error error);
        void AddErrors(IEnumerable<Error> errors);

    }
}
