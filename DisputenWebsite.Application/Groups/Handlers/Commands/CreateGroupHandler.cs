using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, CreateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;

        public CreateGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<CreateGroupCommandResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group { 
                Name = request.Name, 
                Description = request.Description
            };
            await _groupConnector.Create(group);
            return new CreateGroupCommandResult(group);
        }
    }
}
