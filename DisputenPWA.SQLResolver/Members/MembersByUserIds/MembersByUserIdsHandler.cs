using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.SQLResolver.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members.MembersByUserIds
{
    public class MembersByUserIdsHandler : IRequestHandler<MembersByUserIdsRequest, IReadOnlyCollection<Member>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IResolveForMembersService _resolveForMembersService;

        public MembersByUserIdsHandler(
            IMemberRepository memberRepository,
            IResolveForMembersService resolveForMembersService
            )
        {
            _memberRepository = memberRepository;
            _resolveForMembersService = resolveForMembersService;
        }

        public async Task<IReadOnlyCollection<Member>> Handle(MembersByUserIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveMembersByUserIds(request.UserIds, request.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<Member>> ResolveMembersByUserIds(IEnumerable<string> userIds, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var members = await GetMembersByUserIds(userIds, helper);
            if (helper.CanGetGroup())
            {
                var groupIds = members.Select(x => x.GroupId);
                members = await _resolveForMembersService.ResolveGroupsForMembers(members, groupIds, helper, cancellationToken);
            }
            if (helper.CanGetUser())
            {
                members = await _resolveForMembersService.ResolveUsersForMembers(members, userIds, helper, cancellationToken);
            }
            return members.AsReadOnly();
        }

        private async Task<List<Member>> GetMembersByUserIds(IEnumerable<string> userIds, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => userIds.Contains(x.UserId));
            return await _memberRepository.GetAll(queryable, helper);
        }
    }
}
