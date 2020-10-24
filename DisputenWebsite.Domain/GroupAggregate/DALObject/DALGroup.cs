using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.Hierarchy;
using System.Collections.Generic;

namespace DisputenPWA.Domain.GroupAggregate.DALObject
{

    public class DALGroup : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DALAppEvent> AppEvents { get; set; }

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
