using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Queries.Results
{
    public class MemberQueryResult : QueryResult<Member>
    {
        public MemberQueryResult(Member member)
            : base(member)
        {

        }
    }
}