using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper)
        {
            var group = await GetGroup(id, helper);
            if (helper.CanGetAppEvents())
            {
                group.AppEvents = await ResolveAppEvents(new List<Guid> { id }, helper.AppEventPropertyHelper);
            }
            return group;
        }

        public async Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var groups = await GetGroups(groupIds, helper);
            if (helper.CanGetAppEvents())
            {
                var appEvents = AddAppEventsToGroup(groups, groupIds, helper);
            }
            return groups.ToImmutableList();
        }

        private async Task<Group> GetGroup(Guid id, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            return await _groupRepository.GetFirstOrDefault(queryable, helper);
        }

        private async Task<IReadOnlyCollection<Group>> GetGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => groupIds.Contains(x.Id));
            return await _groupRepository.GetAll(queryable, helper);
        }

        private async Task<IReadOnlyCollection<Group>> AddAppEventsToGroup(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var appEvents = await ResolveAppEvents(groupIds, helper.AppEventPropertyHelper);
            foreach (var group in groups)
            {
                group.AppEvents = appEvents.Where(x => x.GroupId == group.Id).ToList();
            }
            return groups;
        }
    }
}
