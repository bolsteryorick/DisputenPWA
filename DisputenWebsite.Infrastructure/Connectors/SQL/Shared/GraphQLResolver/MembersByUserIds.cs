using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public partial class GraphQLResolver
    {
        public async Task<IReadOnlyCollection<Member>> ResolveMembersByUserIds(IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var members = await GetMembersByUserIds(userIds, helper);
            if (helper.CanGetGroup())
            {
                var groupIds = members.Select(x => x.GroupId);
                members = await ResolveGroupsForMembers(members, groupIds, helper);
            }
            if (helper.CanGetUser())
            {
                members = await ResolveUsersForMembers(members, userIds, helper);
            }
            return members.AsReadOnly();
        }

        private async Task<List<Member>> GetMembersByUserIds(IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => userIds.Contains(x.UserId));
            return await _memberRepository.GetAll(queryable, helper);
        }
    }
}