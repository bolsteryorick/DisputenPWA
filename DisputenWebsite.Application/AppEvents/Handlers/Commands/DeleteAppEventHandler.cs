using DisputenPWA.Domain.EventAggregate.Commands;
using DisputenPWA.Domain.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.AppEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class DeleteAppEventHandler
        : IRequestHandler<DeleteAppEventCommand, DeleteAppEventCommandResult>
    {
        private readonly IAppEventConnector _appEventConnector;

        public DeleteAppEventHandler(
            IAppEventConnector appEventConnector
            )
        {
            _appEventConnector = appEventConnector;
        }

        public async Task<DeleteAppEventCommandResult> Handle(DeleteAppEventCommand request, CancellationToken cancellationToken)
        {
            await _appEventConnector.DeleteAppEvent(request.AppEventId);
            return new DeleteAppEventCommandResult(null);
        }
    }
}
