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
        private readonly IMemberConnector _memberConnector;

        public DeleteMembersHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<DeleteMembersCommandResult> Handle(DeleteMembersCommand request, CancellationToken cancellationToken)
        {
            await _memberConnector.Delete(request.GroupId);
            return new DeleteMembersCommandResult(null);
        }
    }
}
