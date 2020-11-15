using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate;
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
            if (helper.CanGetMembers())
            {
                group.Members = await ResolveMembersForGroups(new List<Guid> { id }, helper.MemberPropertyHelper);
            }
            return group;
        }

        public async Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var groups = await GetGroups(groupIds, helper);
            if (helper.CanGetAppEvents())
            {
                groups = await AddAppEventsToGroups(groups, groupIds, helper);
            }
            if (helper.CanGetMembers())
            {
                groups = await AddMembersToGroups(groups, groupIds, helper);
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

        private async Task<IReadOnlyCollection<Group>> AddAppEventsToGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var appEvents = await ResolveAppEvents(groupIds, helper.AppEventPropertyHelper);
            var groupIdToAppEventsDict = GetGroupIdToAppEventDict(appEvents);
            foreach (var group in groups)
            {
                if (groupIdToAppEventsDict.TryGetValue(group.Id, out var groupAppEvents)) group.AppEvents = groupAppEvents;
            }
            return groups;
        }

        private async Task<IReadOnlyCollection<Group>> AddMembersToGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var members = await ResolveMembersForGroups(groupIds, helper.MemberPropertyHelper);
            var groupIdToMembersDict = GetGroupIdToMembersDict(members);
            foreach (var group in groups)
            {
                if (groupIdToMembersDict.TryGetValue(group.Id, out var groupMembers)) group.Members = groupMembers;
            }
            return groups;
        }

        private Dictionary<Guid, List<Member>> GetGroupIdToMembersDict(IReadOnlyCollection<Member> items)
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

        private Dictionary<Guid, List<AppEvent>> GetGroupIdToAppEventDict(IReadOnlyCollection<AppEvent> items)
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
    }
}