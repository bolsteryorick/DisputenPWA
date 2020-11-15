using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.MemberAggregate.Commands;
using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Members;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers.Commands
{

    public class CreateMembersHandler : IRequestHandler<CreateMembersCommand, CreateMembersCommandResult>
    {
        private readonly IMemberConnector _memberConnector;

        public CreateMembersHandler(
            IMemberConnector memberConnector
            )
        {
            _memberConnector = memberConnector;
        }

        public async Task<CreateMembersCommandResult> Handle(CreateMembersCommand request, CancellationToken cancellationToken)
        {
            var members = new List<Member>();
            foreach(var userId in request.UserIds)
            {
                members.Add(new Member
                {
                    UserId = userId,
                    GroupId = request.GroupId,
                });
            }
            await _memberConnector.Create(members.Select(m => m.CreateDalMember()));
            return new CreateMembersCommandResult(null);
        }
    }
}
