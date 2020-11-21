using DisputenPWA.Domain.MemberAggregate.Queries;
using DisputenPWA.Domain.MemberAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Queries
{
    public class MemberQueryHandler : IRequestHandler<MemberQuery, MemberQueryResult>
    {
        private readonly IMemberConnector _memberConnector;

        public MemberQueryHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<MemberQueryResult> Handle(MemberQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberConnector.GetMember(request.MemberId, request.MemberPropertyHelper);
            return new MemberQueryResult(member);
        }
    }
}