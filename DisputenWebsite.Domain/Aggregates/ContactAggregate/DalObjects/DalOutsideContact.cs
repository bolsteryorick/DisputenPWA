using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects
{
    public class DalOutsideContact : IdModelBase
    {
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Contact CreateContact()
        {
            return new Contact
            {
                Id = Id,
                UserId = UserId,
                EmailAddress = EmailAddress
            };
        }
    }
}
