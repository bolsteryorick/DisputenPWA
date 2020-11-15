using DisputenPWA.Domain.MemberAggregate.DalObject;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DisputenPWA.Domain.UserAggregate
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<DalMember> GroupMemberships { get; set; }
    }
}
