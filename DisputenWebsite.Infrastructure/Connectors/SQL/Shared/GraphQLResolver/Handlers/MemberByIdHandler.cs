using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Handlers
{
    public class MemberByIdHandler : IRequestHandler<MemberByIdRequest, MemberByIdResult>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMediator _mediator;

        public MemberByIdHandler(
            IMemberRepository memberRepository,
            IMediator mediator
            )
        {
            _memberRepository = memberRepository;
            _mediator = mediator;
        }

        public async Task<MemberByIdResult> Handle(MemberByIdRequest request, CancellationToken cancellationToken)
        {
            return new MemberByIdResult(
                await ResolveMemberById(request.MemberId, request.Helper, cancellationToken)
                );
        }

        private async Task<Member> ResolveMemberById(Guid id, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var member = await GetMemberById(id, helper);
            if (helper.CanGetGroup())
            {
                member.Group = (await _mediator.Send(new GroupByIdRequest(member.GroupId, helper.GroupPropertyHelper), cancellationToken)).Result;
            }
            if (helper.CanGetUser())
            {
                member.User = (await _mediator.Send(new UserByIdRequest(member.UserId, helper.UserPropertyHelper), cancellationToken)).Result;
            }
            return member;
        }

        private async Task<Member> GetMemberById(Guid id, MemberPropertyHelper helper)
        {
            var queryable = _memberRepository.GetQueryable().Where(x => x.Id == id);
            return await _memberRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}
