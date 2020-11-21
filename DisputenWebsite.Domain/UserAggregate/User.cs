using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.MemberAggregate;
using System.Collections.Generic;

namespace DisputenPWA.Domain.UserAggregate
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IReadOnlyCollection<Member> Memberships { get; set; }
        public IReadOnlyCollection<Attendee> Attendences { get; set; }
        public string JWTToken { get; set; }
    }
}