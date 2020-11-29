using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.Groups.GroupsByIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members
{
    public interface IResolveForMembersService
    {
        Task<IReadOnlyCollection<Member>> Resolve(
            IQueryable<DalMember> query,
            MemberPropertyHelper helper,
            CancellationToken cancellationToken
            );
    }

    public class ResolveForMembersService : IResolveForMembersService
    {
        private readonly IMediator _mediator;
        private readonly IMemberRepository _memberRepository;

        public ResolveForMembersService(
            IMediator mediator,
            IMemberRepository memberRepository
            )
        {
            _mediator = mediator;
            _memberRepository = memberRepository;
        }

        public async Task<IReadOnlyCollection<Member>> Resolve(
            IQueryable<DalMember> query,
            MemberPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var members = await _memberRepository.GetAll(query, helper);
            members = await AddForeignObjects(members, helper, cancellationToken);
            return members.ToImmutableList();
        }

        private async Task<IList<Member>> AddForeignObjects(
            IList<Member> members,
            MemberPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            if (helper.CanGetGroup())
            {
                var groups = await GetGroups(members, helper, cancellationToken);
                members = AddGroupsToMembers(groups, members);
            }
            if (helper.CanGetUser())
            {
                var users = await GetUsers(members, helper, cancellationToken);
                members = AddUsersToMembers(users, members);
            }
            return members;
        }

        private async Task<IReadOnlyCollection<Group>> GetGroups(
            IList<Member> members,
            MemberPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var groupIds = members.Select(x => x.GroupId);
            return await _mediator.Send(new GroupsByIdsRequest(groupIds, helper.GroupPropertyHelper), cancellationToken);
        }

        private async Task<IReadOnlyCollection<User>> GetUsers(
            IList<Member> members,
            MemberPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var userIds = members.Select(x => x.UserId);
            return await _mediator.Send(new UsersByIdsRequest(userIds, helper.UserPropertyHelper), cancellationToken);
        }

        private IList<Member> AddGroupsToMembers(
            IReadOnlyCollection<Group> groups,
            IList<Member> members
            )
        {
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (groupsDictionary.TryGetValue(member.GroupId, out var group)) member.Group = group;
            }
            return members;
        }

        private IList<Member> AddUsersToMembers(
            IReadOnlyCollection<User> users,
            IList<Member> members
            )
        {
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (usersDictionary.TryGetValue(member.UserId, out var user)) member.User = user;
            }
            return members;
        }
    }
}
