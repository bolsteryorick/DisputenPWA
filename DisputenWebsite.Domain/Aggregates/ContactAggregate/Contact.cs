using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate
{
    public class Contact : IdModelBase
    {
        public string UserId { get; set; }
        public string ContactUserId { get; set; }
        public string EmailAddress { get; set; }
        public virtual User User { get; set; }

        public DalPlatformContact CreateDalContact()
        {
            return new DalPlatformContact
            {
                Id = Id,
                UserId = UserId,
                ContactUserId = ContactUserId
            };
        }

        public DalOutsideContact CreateDalOutsideContact()
        {
            return new DalOutsideContact
            {
                Id = Id,
                UserId = UserId,
                EmailAddress = EmailAddress
            };
        }
    }
}
