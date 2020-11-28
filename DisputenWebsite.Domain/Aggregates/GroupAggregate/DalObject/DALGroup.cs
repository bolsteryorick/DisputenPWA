using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject
{

    public class DalGroup : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DalAppEvent> AppEvents { get; set; }
        public virtual ICollection<DalMember> Members { get; set; }

        public Group CreateGroup()
        {
            return new Group
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };
        }
    }
}
