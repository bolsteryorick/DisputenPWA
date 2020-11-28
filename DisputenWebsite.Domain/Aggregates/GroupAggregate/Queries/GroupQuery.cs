using DisputenPWA.Domain.Aggregates.GroupAggregate.Queries.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Queries
{
    public class GroupQuery : IRequest<GroupQueryResult>
    {
        public Guid GroupId { get; }
        public GroupPropertyHelper GroupPropertyHelper { get; }

        public GroupQuery(
            Guid groupId,
            GroupPropertyHelper groupPropertyHelper
            )
        {
            GroupId = groupId;
            GroupPropertyHelper = groupPropertyHelper;
        }
    }
}