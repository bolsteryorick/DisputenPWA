using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class MemberByIdRequest : IRequest<MemberByIdResult>
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
