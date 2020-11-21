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
        public async Task<Member> ResolveMemberById(Guid id, MemberPropertyHelper helper)
        {
            var member = await GetMemberById(id, helper);
            if (helper.CanGetGroup())
            {
                member.Group = await ResolveGroupById(member.GroupId, helper.GroupPropertyHelper);
            }
            if (helper.CanGetUser())
            {
                member.User = await ResolveUserById(member.UserId, helper.UserPropertyHelper);
            }
            return member;
        }

        private async Task<Member> GetMemberById(Guid id, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => x.Id == id);
            return await _memberRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}