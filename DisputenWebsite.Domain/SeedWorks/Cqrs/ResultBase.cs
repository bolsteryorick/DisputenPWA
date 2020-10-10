using DisputenPWA.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.SeedWorks.Cqrs
{
    public class ResultBase<T> : IFailable
    {
        public T Result { get; set; }

        public ResultBase(T result)
        {
            Result = result;
        }

        public ResultBase(Error error)
        {
            _errors.Add(error);
        }

        private readonly List<Error> _errors = new List<Error>();
        
        public IEnumerable<Error> Errors => _errors;

        public void AddError(Error error)
        {
            _errors.Add(error);
        }

        public void AddErrors(IEnumerable<Error> errors)
        {
            _errors.AddRange(errors);
        }
    }
}
