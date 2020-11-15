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
    public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, DeleteMemberCommandResult>
    {
        private readonly IMemberConnector _memberConnector;

        public DeleteMemberHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<DeleteMemberCommandResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _memberConnector.Delete(request.MemberId);
            return new DeleteMemberCommandResult(null);
        }
    }
}
