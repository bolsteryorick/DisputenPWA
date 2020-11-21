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
    public class DeleteMembersHandler : IRequestHandler<DeleteMembersCommand, DeleteMembersCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IMemberConnector _memberConnector;

        public DeleteMembersHandler(
            IOperationAuthorizer operationAuthorizer,
            IMemberConnector memberConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _memberConnector = memberConnector;
        }

        public async Task<DeleteMembersCommandResult> Handle(DeleteMembersCommand request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanUpdateGroup(request.GroupId))
            {
                await _memberConnector.Delete(request.GroupId);
            }
            return new DeleteMembersCommandResult(null);
        }
    }
}
