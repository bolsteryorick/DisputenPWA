using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate.Queries.Results
{
    public class JwtTokenQueryResult : QueryResult<User>
    {
        public JwtTokenQueryResult(User user) : base(user)
        {

        }
    }
}
