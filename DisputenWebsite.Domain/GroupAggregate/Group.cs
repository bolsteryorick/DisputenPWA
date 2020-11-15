using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.MemberAggregate;
using System.Collections.Generic;

namespace DisputenPWA.Domain.GroupAggregate
{
    public class Group : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IReadOnlyCollection<AppEvent> AppEvents { get; set; }
        public IReadOnlyCollection<Member> Members { get; set; }

        public DalGroup CreateDalGroup()
        {
            return new DalGroup
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };
        }
    }
}