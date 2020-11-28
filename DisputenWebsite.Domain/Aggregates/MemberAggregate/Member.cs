using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate
{
    public class Member : IdModelBase
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public Guid GroupId { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }

        public DalMember CreateDalMember()
        {
            return new DalMember
            {
                Id = Id,
                UserId = UserId,
                IsAdmin = IsAdmin,
                GroupId = GroupId
            };
        }
    }
}
