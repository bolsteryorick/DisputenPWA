using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using MediatR;
using System;

namespace DisputenPWA.Domain.GroupAggregate.Queries
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