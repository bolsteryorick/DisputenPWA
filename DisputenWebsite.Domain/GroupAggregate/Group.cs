using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
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
        public IReadOnlyCollection<AppEvent> AppEvents { get; set; }

        public DALGroup CreateDALGroup()
        {
            return new DALGroup
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };
        }
    }
}