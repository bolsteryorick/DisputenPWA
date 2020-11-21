using DisputenPWA.Domain.MemberAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Members.MembersByGroupIds
{
    public class MembersByGroupIdsRequest : IRequest<IReadOnlyCollection<Member>>
    {
        public MembersByGroupIdsRequest(
            IEnumerable<Guid> groupIds,
            MemberPropertyHelper helper
            )
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public MemberPropertyHelper Helper { get; }
    }
}
