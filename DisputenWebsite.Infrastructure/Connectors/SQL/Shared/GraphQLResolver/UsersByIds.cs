using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public partial class GraphQLResolver
    {
        public async Task<IReadOnlyCollection<User>> ResolveUsersByIds(IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var users = await GetUsersByIds(userIds, helper);
            if (helper.CanGetMembers())
            {
                users = await ResolveMembersForUsers(users, userIds, helper);
            }
            return users.ToList();
        }

        private async Task<IEnumerable<User>> GetUsersByIds(IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => userIds.Contains(x.Id));
            return await _userRepository.GetAll(queryable, helper);
        }

        private async Task<IEnumerable<User>> ResolveMembersForUsers(IEnumerable<User> users, IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var memberships = await ResolveMembersByUserIds(userIds, helper.MembershipsPropertyHelper);
            var userIdToMembershipDict = GetUserIdToMembershipsDict(memberships);
            foreach (var user in users)
            {
                if (userIdToMembershipDict.TryGetValue(user.Id, out var userMemberships)) user.Memberships = userMemberships;
            }
            return users;
        }

        private Dictionary<string, List<Member>> GetUserIdToMembershipsDict(IReadOnlyCollection<Member> items)
        {
            var dict = new Dictionary<string, List<Member>>();
            foreach (var item in items)
            {
                var userId = item.UserId;
                if (!dict.ContainsKey(userId))
                {
                    dict[userId] = new List<Member>();
                }
                dict[userId].Add(item);
            }
            return dict;
        }
    }
}
