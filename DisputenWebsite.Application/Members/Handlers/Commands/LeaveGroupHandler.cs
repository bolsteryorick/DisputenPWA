using DisputenPWA.Application.Services;
using DisputenPWA.Domain.MemberAggregate.Commands;
using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Commands
{
    public class LeaveGroupHandler : IRequestHandler<LeaveGroupCommand, DeleteMemberCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IMemberConnector _memberConnector;

        public LeaveGroupHandler(
            IOperationAuthorizer operationAuthorizer,
            IMemberConnector memberConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _memberConnector = memberConnector;
        }

        public async Task<DeleteMemberCommandResult> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _operationAuthorizer.CanLeaveGroup(request.MemberId))
            {
                await _memberConnector.Delete(request.MemberId);
            }
            return new DeleteMemberCommandResult(null);
        }
    }
}
