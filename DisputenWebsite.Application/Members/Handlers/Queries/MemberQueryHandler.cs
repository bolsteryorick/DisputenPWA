using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Queries;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Queries
{
    public class MemberQueryHandler : IRequestHandler<MemberQuery, MemberQueryResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IMemberConnector _memberConnector;

        public MemberQueryHandler(
            IOperationAuthorizer operationAuthorizer,
            IMemberConnector memberConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _memberConnector = memberConnector;
        }

        public async Task<MemberQueryResult> Handle(MemberQuery request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanQueryMember(request.MemberId))
            {
                var member = await _memberConnector.GetMember(request.MemberId, request.MemberPropertyHelper);
                return new MemberQueryResult(member);
            }
            return new MemberQueryResult(null);
        }
    }
}