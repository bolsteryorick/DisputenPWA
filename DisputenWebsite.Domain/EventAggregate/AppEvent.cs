using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate
{
    public class AppEvent : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public DALAppEvent CreateDALAppEvent()
        {
            return new DALAppEvent
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
