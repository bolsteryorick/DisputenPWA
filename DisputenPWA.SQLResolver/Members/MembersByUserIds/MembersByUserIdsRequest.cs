using DisputenPWA.Domain.MemberAggregate;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Members.MembersByUserIds
{
    public class MembersByUserIdsRequest : IRequest<IReadOnlyCollection<Member>>
    {
        public MembersByUserIdsRequest(
            IEnumerable<string> userIds,
            MemberPropertyHelper helper
            )
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public MemberPropertyHelper Helper { get; }
    }
}
