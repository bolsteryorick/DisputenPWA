using DisputenPWA.Application.Base;
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
    public class UpdateMemberHandler : UpdateHandlerBase, IRequestHandler<UpdateMemberCommand, UpdateMemberCommandResult>
    {
        private readonly IMemberConnector _memberConnector;

        public UpdateMemberHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<UpdateMemberCommandResult> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var properties = GetUpdateProperties(request);
            var member = await _memberConnector.UpdateProperties(properties, request.MemberId);
            return new UpdateMemberCommandResult(member);
        }
    }
}