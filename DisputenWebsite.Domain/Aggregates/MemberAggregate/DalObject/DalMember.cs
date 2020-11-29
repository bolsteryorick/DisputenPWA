using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject
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
                UserId = UserId,
                IsAdmin = IsAdmin,
                GroupId = GroupId
            };
        }
    }
}