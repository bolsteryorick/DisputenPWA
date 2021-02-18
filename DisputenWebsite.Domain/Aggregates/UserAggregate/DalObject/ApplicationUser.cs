using DisputenPWA.DAL.Models;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.DalObject
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<DalMember> GroupMemberships { get; set; }
        public virtual ICollection<DalAttendee> Attendences { get; set; }
        public virtual ICollection<DalPlatformContact> PlatformContacts { get; set; }
        public virtual ICollection<DalPlatformContact> PlatformContactReferences { get; set; }
        public virtual ICollection<DalOutsideContact> OutsideContacts { get; set; }
        public virtual ICollection<DalRefreshToken> RefreshTokens { get; set; }

        public User CreateUser()
        {
            return new User
            {
                Id = Id,
                Email = Email,
                UserName = UserName
            };
        }
    }
}
