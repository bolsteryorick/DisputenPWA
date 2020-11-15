using DisputenPWA.Domain.MemberAggregate;
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
    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, CreateMemberCommandResult>
    {
        private readonly IMemberConnector _memberConnector;

        public CreateMemberHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<CreateMemberCommandResult> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
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
