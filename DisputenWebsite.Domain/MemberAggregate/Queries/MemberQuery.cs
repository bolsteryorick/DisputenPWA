using DisputenPWA.Domain.MemberAggregate.Queries.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.MemberAggregate.Queries
{
    public class MemberQuery : IRequest<MemberQueryResult>
    {
        public MemberQuery(
            Guid memberId,
            MemberPropertyHelper memberPropertyHelper
            )
        {
            MemberId = memberId;
            MemberPropertyHelper = memberPropertyHelper;
        }

        public Guid MemberId { get; }
        public MemberPropertyHelper MemberPropertyHelper { get; }
    }
}