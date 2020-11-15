using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate.Queries.Results
{
    public class GetJwtTokenQueryResult : QueryResult<User>
    {
        public GetJwtTokenQueryResult(User user) : base(user)
        {

        }
    }
}
