using System;
using System.Collections.Generic;
using System.Text;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;

namespace DisputenPWA.Domain.Aggregates.GoogleAccessInfoAggregate
{
    public class GoogleAccessInfo : IdModelBase
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}