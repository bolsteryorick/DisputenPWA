using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.EventAggregate
{
    public class AppEvent : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public DalAppEvent CreateDALAppEvent()
        {
            return new DalAppEvent
            {
                Id = Id,
                Name = Name,
                Description = Description,
                StartTime = StartTime,
                EndTime = EndTime,
                GroupId = GroupId
            };
        }

        // group and groupId
        // participants
        // document
        // notification?
        // location
    }
}
