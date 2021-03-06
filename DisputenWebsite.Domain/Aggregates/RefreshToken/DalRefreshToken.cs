using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.DAL.Models
{
    public class DalRefreshToken : IdModelBase
    {
        public string UserId { get; set; }
        public string RefreshTokenHash { get; set; }
        public byte[] RefreshTokenSalt { get; set; }
        public string AppInstanceId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}