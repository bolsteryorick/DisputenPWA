using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.EventAggregate.DALObject
{
    public class DALAppEvent : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid GroupId { get; set; }
        public virtual DALGroup Group { get; set; }

        public AppEvent CreateAppEvent()
        {
            return new AppEvent
            {
                Id = Id,
                Name = Name,
                Description = Description,
                StartTime = StartTime,
                EndTime = EndTime,
                GroupId = GroupId
            };
        }
    }
}
