using DisputenPWA.Domain.Aggregates.MemberAggregate;
using MediatR;
using System;

namespace DisputenPWA.SQLResolver.Members.MemberById
{
    public class MemberByIdRequest : IRequest<Member>
    {
        public MemberByIdRequest(
            Guid memberId,
            MemberPropertyHelper helper
            )
        {
            MemberId = memberId;
            Helper = helper;
        }

        public Guid MemberId { get; }
        public MemberPropertyHelper Helper { get; }
    }
}
