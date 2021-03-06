using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.UserAggregate
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool? IsContact { get; set; }
        public IReadOnlyCollection<Member> Memberships { get; set; }
        public IReadOnlyCollection<Attendee> Attendences { get; set; }
        public IReadOnlyCollection<Contact> Contacts { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}