using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IReadOnlyCollection<Member> Memberships { get; set; }
        public string JWTToken { get; set; }
    }
}