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
        public async Task<User> ResolveUserById(string userId, UserPropertyHelper helper)
        {
            var user = await GetUserById(userId, helper);
            if (helper.CanGetMembers())
            {
                user.Memberships = await ResolveMembersByUserIds(new List<string> { userId }, helper.MembershipsPropertyHelper);
            }
            return user;
        }

       

        private async Task<User> GetUserById(string id, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => x.Id == id);
            return await _userRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}