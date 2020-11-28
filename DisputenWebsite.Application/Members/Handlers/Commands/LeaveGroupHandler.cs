﻿using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Commands
{
    public class LeaveGroupHandler : IRequestHandler<LeaveGroupCommand, DeleteMemberCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IMemberConnector _memberConnector;
        private readonly ILeaveAllGroupEventsService _leaveAllGroupEventsService;

        public LeaveGroupHandler(
            IOperationAuthorizer operationAuthorizer,
            IMemberConnector memberConnector,
            ILeaveAllGroupEventsService leaveAllGroupEventsService
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _memberConnector = memberConnector;
            _leaveAllGroupEventsService = leaveAllGroupEventsService;
        }

        public async Task<DeleteMemberCommandResult> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _operationAuthorizer.CanLeaveGroup(request.MemberId))
            {
                await _leaveAllGroupEventsService.LeaveAllGroupEvents(request.MemberId);
                await _memberConnector.Delete(request.MemberId);
            }
            return new DeleteMemberCommandResult(null);
        }
    }
}
