using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Queries.Results
{
    public class GetGroupQueryResult : QueryResult<Group>
    {
        public GetGroupQueryResult(Group group)
            : base(group)
        {

        }
    }
}