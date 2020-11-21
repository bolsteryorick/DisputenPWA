using DisputenPWA.Application.Base;
using DisputenPWA.Application.Services;
using DisputenPWA.Domain.GroupAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class UpdateGroupHandler : UpdateHandlerBase, IRequestHandler<UpdateGroupCommand, UpdateGroupCommandResult>
    {
        private readonly IGroupConnector _groupConnector;
        private readonly IOperationAuthorizer _operationAuthorizer;

        public UpdateGroupHandler(
            IGroupConnector groupConnector,
            IOperationAuthorizer operationAuthorizer
            )
        {
            _groupConnector = groupConnector;
            _operationAuthorizer = operationAuthorizer;
        }

        public async Task<UpdateGroupCommandResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanUpdateGroup(request.Id))
            {
                var properties = GetUpdateProperties(request);
                var group = await _groupConnector.UpdateProperties(properties, request.Id);
                return new UpdateGroupCommandResult(group);
            }
            return new UpdateGroupCommandResult(null);
        }
    }
}