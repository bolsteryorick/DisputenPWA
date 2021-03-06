using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Queries.Results
{
    public class AllContactsQueryResult : QueryResult<IEnumerable<Contact>>
    {
        public AllContactsQueryResult(IEnumerable<Contact> result) : base(result)
        {
        }
    }
}
