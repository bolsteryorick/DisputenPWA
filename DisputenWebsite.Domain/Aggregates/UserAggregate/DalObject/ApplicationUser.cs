using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.DalObject
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<DalMember> GroupMemberships { get; set; }
        public virtual ICollection<DalAttendee> Attendences { get; set; }
    }
}
