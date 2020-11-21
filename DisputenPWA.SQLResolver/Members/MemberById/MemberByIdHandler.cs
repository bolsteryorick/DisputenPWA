using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.SQLResolver.Groups.GroupById;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Members.MemberById
{
    public class MemberByIdHandler : IRequestHandler<MemberByIdRequest, Member>
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

        public async Task<Member> Handle(MemberByIdRequest request, CancellationToken cancellationToken)
        {
            return await ResolveMemberById(request.MemberId, request.Helper, cancellationToken);
        }

        private async Task<Member> ResolveMemberById(Guid id, MemberPropertyHelper helper, CancellationToken cancellationToken)
        {
            var member = await GetMemberById(id, helper);
            if (helper.CanGetGroup())
            {
                member.Group = await _mediator.Send(new GroupByIdRequest(member.GroupId, helper.GroupPropertyHelper), cancellationToken);
            }
            if (helper.CanGetUser())
            {
                member.User = await _mediator.Send(new UserByIdRequest(member.UserId, helper.UserPropertyHelper), cancellationToken);
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
