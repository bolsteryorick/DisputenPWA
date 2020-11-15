using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.MemberAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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