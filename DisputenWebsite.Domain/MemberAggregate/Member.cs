using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.MemberAggregate.DalObject;
using DisputenPWA.Domain.UserAggregate;
using System;

namespace DisputenPWA.Domain.MemberAggregate
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
                IsAdmin = IsAdmin,
                UserId = UserId,
                GroupId = GroupId
            };
        }
    }
}
