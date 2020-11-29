using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
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

        public async Task<IReadOnlyCollection<Member>> Handle(MembersByUserIdsRequest req, CancellationToken cancellationToken)
        {
            var query = _memberRepository.GetQueryable().Where(x => req.UserIds.Contains(x.UserId));
            return await _resolveForMembersService.Resolve(query, req.Helper, cancellationToken);
        }
    }
}
