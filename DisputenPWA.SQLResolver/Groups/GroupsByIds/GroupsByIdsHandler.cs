using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsFromGroupsIds;
using DisputenPWA.SQLResolver.Members.MembersByGroupIds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Groups.GroupsByIds
{
    public class GroupsByIdsHandler : IRequestHandler<GroupsByIdsRequest, IReadOnlyCollection<Group>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;

        public GroupsByIdsHandler(
            IGroupRepository groupRepository,
            IMediator mediator
            )
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task<IReadOnlyCollection<Group>> Handle(GroupsByIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveGroupsByIds(request.GroupIds, request.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<Group>> ResolveGroupsByIds(IEnumerable<Guid> groupIds, GroupPropertyHelper helper, CancellationToken cancellationToken)
        {
            var groups = await GetGroupsByIds(groupIds, helper);
            if (helper.CanGetAppEvents())
            {
                groups = await ResolveAppEventsForGroups(groups, groupIds, helper, cancellationToken);
            }
            if (helper.CanGetMembers())
            {
                groups = await ResolveMembersForGroups(groups, groupIds, helper, cancellationToken);
            }
            return groups.ToImmutableList();
        }

        private async Task<IReadOnlyCollection<Group>> GetGroupsByIds(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => groupIds.Contains(x.Id));
            return await _groupRepository.GetAll(queryable, helper);
        }

        private async Task<IReadOnlyCollection<Group>> ResolveAppEventsForGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper, CancellationToken cancellationToken)
        {
            var appEvents = await _mediator.Send(new AppEventsFromGroupIdsRequest(groupIds, helper.AppEventPropertyHelper), cancellationToken);
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

        private async Task<IReadOnlyCollection<Group>> ResolveMembersForGroups(IReadOnlyCollection<Group> groups, IEnumerable<Guid> groupIds, GroupPropertyHelper helper, CancellationToken cancellationToken)
        {
            var members = await _mediator.Send(new MembersByGroupIdsRequest(groupIds, helper.MemberPropertyHelper), cancellationToken);
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
