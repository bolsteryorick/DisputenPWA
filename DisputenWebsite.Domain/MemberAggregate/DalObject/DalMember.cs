using DisputenPWA.Domain.GroupAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.UserAggregate;
using System;

namespace DisputenPWA.Domain.MemberAggregate.DalObject
{
    public class DalMember : IdModelBase
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public Guid GroupId { get; set; }
        public virtual DalGroup Group { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Member CreateMember()
        {
            return new Member
            {
                Id = Id,
                IsAdmin = IsAdmin,
                UserId = UserId,
                GroupId = GroupId
            };
        }
    }
}