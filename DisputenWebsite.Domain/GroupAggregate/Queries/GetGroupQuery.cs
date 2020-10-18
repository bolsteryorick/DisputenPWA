using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Queries
{
    public class GetGroupQuery : IRequest<GetGroupQueryResult>
    {
        public Guid GroupId { get; }

        public GetGroupQuery(Guid groupId)
        {
            GroupId = groupId;
        }
    }
}