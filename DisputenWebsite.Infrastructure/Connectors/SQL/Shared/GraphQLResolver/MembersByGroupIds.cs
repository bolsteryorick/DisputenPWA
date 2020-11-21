using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public partial class GraphQLResolver
    {
        public async Task<IReadOnlyCollection<Member>> ResolveMembersByGroupIds(IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var members = await GetMembersByGroupIds(groupIds, helper);
            if (helper.CanGetGroup())
            {
                members = await ResolveGroupsForMembers(members, groupIds, helper);
            }
            if (helper.CanGetUser())
            {
                var userIds = members.Select(x => x.UserId);
                members = await ResolveUsersForMembers(members, userIds, helper);
            }
            return members.AsReadOnly();
        }

        private async Task<List<Member>> GetMembersByGroupIds(IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => groupIds.Contains(x.GroupId));
            return await _memberRepository.GetAll(queryable, helper);
        }

        private async Task<List<Member>> ResolveGroupsForMembers(List<Member> members, IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var groups = await ResolveGroupsByIds(groupIds, helper.GroupPropertyHelper);
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (groupsDictionary.TryGetValue(member.GroupId, out var group)) member.Group = group;
            }
            return members;
        }

        private async Task<List<Member>> ResolveUsersForMembers(List<Member> members, IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var users = await ResolveUsersByIds(userIds, helper.UserPropertyHelper);
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (usersDictionary.TryGetValue(member.UserId, out var user)) member.User = user;
            }
            return members;
        }
    }
}
