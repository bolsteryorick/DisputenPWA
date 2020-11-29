using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Hierarchy;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate
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