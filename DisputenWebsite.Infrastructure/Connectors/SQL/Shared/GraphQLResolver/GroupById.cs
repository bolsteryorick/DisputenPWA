using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public partial class GraphQLResolver
    {
        public async Task<Group> ResolveGroupById(Guid id, GroupPropertyHelper helper)
        {
            var group = await GetGroupById(id, helper);
            if (helper.CanGetAppEvents())
            {
                group.AppEvents = await ResolveAppEventsFromGroupIds(new List<Guid> { id }, helper.AppEventPropertyHelper);
            }
            if (helper.CanGetMembers())
            {
                group.Members = await ResolveMembersByGroupIds(new List<Guid> { id }, helper.MemberPropertyHelper);
            }
            return group;
        }
        
        private async Task<Group> GetGroupById(Guid id, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            return await _groupRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}