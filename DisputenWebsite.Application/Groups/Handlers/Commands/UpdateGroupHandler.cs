using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.Groups;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, UpdateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;

        public UpdateGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<UpdateGroupCommandResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group { Id = request.Id, Name = request.Name, Description = request.Description };
            await _groupConnector.UpdateGroup(group);
            return new UpdateGroupCommandResult(group);
        }
    }
}
