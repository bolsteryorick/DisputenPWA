using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members.MemberById
{
    public class MemberByIdHandler : IRequestHandler<MemberByIdRequest, Member>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IResolveForMembersService _resolveForMembersService;

        public MemberByIdHandler(
            IMemberRepository memberRepository,
            IResolveForMembersService resolveForMembersService
            )
        {
            _memberRepository = memberRepository;
            _resolveForMembersService = resolveForMembersService;
        }

        public async Task<Member> Handle(MemberByIdRequest req, CancellationToken cancellationToken)
        {
            var query = _memberRepository.GetQueryable().Where(x => x.Id == req.MemberId);
            var memberInList = await _resolveForMembersService.Resolve(query, req.Helper, cancellationToken);
            return memberInList.FirstOrDefault();
        }
    }
}
