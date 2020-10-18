using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate
{
    public class Group : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
