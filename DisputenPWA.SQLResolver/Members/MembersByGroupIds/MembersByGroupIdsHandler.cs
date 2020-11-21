using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.SQLResolver.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members.MembersByGroupIds
{
    public class MembersByGroupIdsHandler : IRequestHandler<MembersByGroupIdsRequest, IReadOnlyCollection<Member>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IResolveForMembersService _resolveForMembersService;

        public MembersByGroupIdsHandler(
            IMemberRepository memberRepository,
            IResolveForMembersService resolveForMembersService
            )
        {
            _memberRepository = memberRepository;
            _resolveForMembersService = resolveForMembersService;
        }

        public async Task<IReadOnlyCollection<Member>> Handle(MembersByGroupIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveMembersByGroupIds(request.GroupIds, request.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<Member>> ResolveMembersByGroupIds(IEnumerable<Guid> groupIds, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var members = await GetMembersByGroupIds(groupIds, helper);
            if (helper.CanGetGroup())
            {
                members = await _resolveForMembersService.ResolveGroupsForMembers(members, groupIds, helper, cancellationToken);
            }
            if (helper.CanGetUser())
            {
                var userIds = members.Select(x => x.UserId);
                members = await _resolveForMembersService.ResolveUsersForMembers(members, userIds, helper, cancellationToken);
            }
            return members.AsReadOnly();
        }

        private async Task<List<Member>> GetMembersByGroupIds(IEnumerable<Guid> groupIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => groupIds.Contains(x.GroupId));
            return await _memberRepository.GetAll(queryable, helper);
        }
    }
}
