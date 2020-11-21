using DisputenPWA.Domain.GroupAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Groups.GroupsByIds
{
    public class GroupsByIdsRequest : IRequest<IReadOnlyCollection<Group>>
    {
        public GroupsByIdsRequest(
            IEnumerable<Guid> groupIds,
            GroupPropertyHelper helper
            )
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public GroupPropertyHelper Helper { get; }
    }
}
