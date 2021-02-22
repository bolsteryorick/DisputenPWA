using DisputenPWA.Domain.Aggregates.ContactAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Queries
{
    public class AllContactsQuery : IRequest<AllContactsQueryResult>
    {
    }
}
