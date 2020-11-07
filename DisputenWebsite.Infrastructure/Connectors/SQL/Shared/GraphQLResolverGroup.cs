using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            var group = await _groupRepository.GetFirstOrDefault(queryable, helper);
            if (helper.CanGetAppEvents())
            {
                var appEvents = await ResolveAppEvents(new List<Guid> { id }, helper.AppEventPropertyHelper);
                group.AppEvents = appEvents;
            }
            return group;
        }

        public async Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => groupIds.Contains(x.Id));
            var groups = await _groupRepository.GetAll(queryable, helper);
            if (helper.CanGetAppEvents())
            {
                var appEvents = await ResolveAppEvents(groupIds, helper.AppEventPropertyHelper);
                foreach (var group in groups)
                {
                    group.AppEvents = appEvents.Where(x => x.GroupId == group.Id).ToList();
                }
            }
            return groups;
        }
    }
}
