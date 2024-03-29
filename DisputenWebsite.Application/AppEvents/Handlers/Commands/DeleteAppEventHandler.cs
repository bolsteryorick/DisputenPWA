﻿using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class DeleteAppEventHandler
        : IRequestHandler<DeleteAppEventCommand, DeleteAppEventCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAppEventConnector _appEventConnector;

        public DeleteAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
        }

        public async Task<DeleteAppEventCommandResult> Handle(DeleteAppEventCommand request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanChangeAppEvent(request.AppEventId))
            {
                return new DeleteAppEventCommandResult(null);
            }
            await _appEventConnector.DeleteAppEvent(request.AppEventId);
            return new DeleteAppEventCommandResult(null);
        }
    }
}
