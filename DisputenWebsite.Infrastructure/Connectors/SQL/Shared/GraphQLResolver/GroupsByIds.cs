using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
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
        public async Task<IReadOnlyCollection<Group>> ResolveGroupsByIds(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var groups = await GetGroupsByIds(groupIds, helper);
            if (helper.CanGetAppEvents())
            {
                groups = await ResolveAppEventsForGroups(groups, groupIds, helper);
            }
            if (helper.CanGetMembers())
            {
                groups = await ResolveMembersForGroups(groups, groupIds, helper);
            }
            return groups.ToImmutableList();
        }

        private async Task<IReadOnlyCollection<Group>> GetGroupsByIds(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => groupIds.Contains(x.Id));
            return await _groupRepository.GetAll(queryable, helper);
        }

        private async Task<IReadOnlyCollection<Group>> ResolveAppEventsForGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var appEvents = await ResolveAppEventsFromGroupIds(groupIds, helper.AppEventPropertyHelper);
            var groupIdToAppEventsDict = MakeGroupIdToAppEventDict(appEvents);
            foreach (var group in groups)
            {
                if (groupIdToAppEventsDict.TryGetValue(group.Id, out var groupAppEvents)) group.AppEvents = groupAppEvents;
            }
            return groups;
        }

        private Dictionary<Guid, List<AppEvent>> MakeGroupIdToAppEventDict(IReadOnlyCollection<AppEvent> items)
        {
            var dict = new Dictionary<Guid, List<AppEvent>>();
            foreach (var item in items)
            {
                var groupId = item.GroupId;
                if (!dict.ContainsKey(groupId))
                {
                    dict[groupId] = new List<AppEvent>();
                }
                dict[groupId].Add(item);
            }
            return dict;
        }

        private async Task<IReadOnlyCollection<Group>> ResolveMembersForGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var members = await ResolveMembersByGroupIds(groupIds, helper.MemberPropertyHelper);
            var groupIdToMembersDict = MakeGroupIdToMembersDict(members);
            foreach (var group in groups)
            {
                if (groupIdToMembersDict.TryGetValue(group.Id, out var groupMembers)) group.Members = groupMembers;
            }
            return groups;
        }

        private Dictionary<Guid, List<Member>> MakeGroupIdToMembersDict(IReadOnlyCollection<Member> items)
        {
            var dict = new Dictionary<Guid, List<Member>>();
            foreach (var item in items)
            {
                var groupId = item.GroupId;
                if (!dict.ContainsKey(groupId))
                {
                    dict[groupId] = new List<Member>();
                }
                dict[groupId].Add(item);
            }
            return dict;
        }
    }
}
