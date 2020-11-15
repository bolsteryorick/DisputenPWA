using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.MemberAggregate.DalObject;
using System.Collections.Generic;

namespace DisputenPWA.Domain.GroupAggregate.DalObject
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
