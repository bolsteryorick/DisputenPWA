using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Hierarchy
{
    public interface IIdModelBase
    {
        Guid Id { get; set; }
    }
}
