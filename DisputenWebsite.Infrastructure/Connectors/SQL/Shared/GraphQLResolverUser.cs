using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<User> ResolveUser(string userId, UserPropertyHelper helper)
        {
            var user = await GetUser(userId, helper);
            if (helper.CanGetMembers())
            {
                user.Memberships = await ResolveMembershipsForUsers(new List<string> { userId }, helper.MembershipsPropertyHelper);
            }
            return user;
        }

        public async Task<IReadOnlyCollection<User>> ResolveUsers(IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var users = await GetUsers(userIds, helper);
            if (helper.CanGetMembers())
            {
                users = await AddMembershipsToUsers(users, userIds, helper);
            }
            return users.ToList();
        }

        private async Task<User> GetUser(string id, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => x.Id == id);
            return await _userRepository.GetFirstOrDefault(queryable, helper);
        }

        private async Task<IEnumerable<User>> GetUsers(IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => userIds.Contains(x.Id));
            return await _userRepository.GetAll(queryable, helper);
        }

        private async Task<IEnumerable<User>> AddMembershipsToUsers(IEnumerable<User> users, IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var memberships = await ResolveMembershipsForUsers(userIds, helper.MembershipsPropertyHelper);
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