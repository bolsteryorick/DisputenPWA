using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.SQLResolver.Groups.GroupsByIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members
{
    public interface IResolveForMembersService
    {
        Task<List<Member>> ResolveGroupsForMembers(List<Member> members, IEnumerable<Guid> groupIds, MemberPropertyHelper helper, CancellationToken cancellationToken);
        Task<List<Member>> ResolveUsersForMembers(List<Member> members, IEnumerable<string> userIds, MemberPropertyHelper helper, CancellationToken cancellationToken);
    }

    public class ResolveForMembersService : IResolveForMembersService
    {
        private readonly IMediator _mediator;

        public ResolveForMembersService(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<List<Member>> ResolveGroupsForMembers(List<Member> members, IEnumerable<Guid> groupIds, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var groups = await _mediator.Send(new GroupsByIdsRequest(groupIds, helper.GroupPropertyHelper), cancellationToken);
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (groupsDictionary.TryGetValue(member.GroupId, out var group)) member.Group = group;
            }
            return members;
        }

        public async Task<List<Member>> ResolveUsersForMembers(List<Member> members, IEnumerable<string> userIds, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new UsersByIdsRequest(userIds, helper.UserPropertyHelper), cancellationToken);
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var member in members)
            {
                if (usersDictionary.TryGetValue(member.UserId, out var user)) member.User = user;
            }
            return members;
        }
    }
}
