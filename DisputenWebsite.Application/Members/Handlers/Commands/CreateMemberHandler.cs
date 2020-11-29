using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Commands
{
    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, CreateMemberCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IMemberConnector _memberConnector;

        public CreateMemberHandler(
            IOperationAuthorizer operationAuthorizer,
            IMemberConnector memberConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _memberConnector = memberConnector;
        }

        public async Task<CreateMemberCommandResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanUpdateGroup(request.GroupId))
            {
                return new CreateMemberCommandResult(null);
            }
            var member = new Member
            {
                UserId = request.UserId,
                GroupId = request.GroupId,
                IsAdmin = request.IsAdmin
            };
            await _memberConnector.Create(member);
            return new CreateMemberCommandResult(member);
        }
    }
}
