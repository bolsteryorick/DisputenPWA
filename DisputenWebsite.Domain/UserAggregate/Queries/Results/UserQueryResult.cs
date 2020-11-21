using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate.Queries.Results
{
    public class UserQueryResult : QueryResult<User>
    {
        public UserQueryResult(User user) : base(user)
        {

        }
    }
}
