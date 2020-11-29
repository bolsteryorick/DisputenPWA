using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsFromGroupsIds;
using DisputenPWA.SQLResolver.Helpers;
using DisputenPWA.SQLResolver.Members.MembersByGroupIds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Groups
{
    public interface IResolveForGroupsService
    {
        Task<IReadOnlyCollection<Group>> Resolve(
            IQueryable<DalGroup> query,
            GroupPropertyHelper helper,
            CancellationToken cancellationToken);
    }

    public class ResolveForGroupsService : IResolveForGroupsService
    {
        private readonly IMediator _mediator;
        private readonly IGroupRepository _groupRepository;

        public ResolveForGroupsService(
            IMediator mediator,
            IGroupRepository groupRepository
            )
        {
            _mediator = mediator;
            _groupRepository = groupRepository;
        }

        public async Task<IReadOnlyCollection<Group>> Resolve(
            IQueryable<DalGroup> query,
            GroupPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var groups = await _groupRepository.GetAll(query, helper);
            groups = await AddForeignObjects(groups, helper, cancellationToken);
            return groups.ToImmutableList();
        }

        private async Task<IList<Group>> AddForeignObjects(
            IList<Group> groups,
            GroupPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            if (helper.CanGetAppEvents())
            {
                var events = await GetEvents(groups, helper, cancellationToken);
                groups = AddAppEventsToGroup(events, groups);
            }
            if (helper.CanGetMembers())
            {
                var members = await GetMembers(groups, helper, cancellationToken);
                groups = AddMembersToGroup(members, groups);
            }
            return groups;
        }

        private async Task<IReadOnlyCollection<AppEvent>> GetEvents(
            IList<Group> groups,
            GroupPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var groupIds = groups.Select(x => x.Id);
            return await _mediator.Send(new AppEventsFromGroupIdsRequest(groupIds, helper.AppEventPropertyHelper), cancellationToken);
        }

        private async Task<IReadOnlyCollection<Member>> GetMembers(
            IList<Group> groups,
            GroupPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var groupIds = groups.Select(x => x.Id);
            return await _mediator.Send(new MembersByGroupIdsRequest(groupIds, helper.MemberPropertyHelper), cancellationToken);
        }

        private IList<Group> AddAppEventsToGroup(
            IReadOnlyCollection<AppEvent> events,
            IList<Group> groups
            )
        {
            var groupIdToAppEventsDict = DictionaryMaker.MakeDictionary<Guid, AppEvent>(nameof(AppEvent.GroupId), events);
            foreach (var group in groups)
            {
                if (groupIdToAppEventsDict.TryGetValue(group.Id, out var groupAppEvents)) group.AppEvents = groupAppEvents;
            }
            return groups;
        }

        private IList<Group> AddMembersToGroup(
            IReadOnlyCollection<Member> members,
            IList<Group> groups
            )
        {
            var groupIdToMembersDict = DictionaryMaker.MakeDictionary<Guid, Member>(nameof(Member.GroupId), members);
            foreach (var group in groups)
            {
                if (groupIdToMembersDict.TryGetValue(group.Id, out var groupMembers)) group.Members = groupMembers;
            }
            return groups;
        }
    }
}