using DisputenPWA.Domain.GroupAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, DeleteGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;

        public DeleteGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<DeleteGroupCommandResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            await _groupConnector.DeleteGroup(request.GroupId);
            return new DeleteGroupCommandResult(null);
        }
    }
}