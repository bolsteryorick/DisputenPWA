using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Hierarchy
{
    public class IdModelBase : IIdModelBase
    {
        public Guid Id { get; set; }
        public IdModelBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
