using DisputenPWA.Domain.EventAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Commands
{
    public class DeleteAppEventCommand : IRequest<DeleteAppEventCommandResult>
    {
        public DeleteAppEventCommand(Guid appEventId)
        {
            AppEventId = appEventId;
        }

        public Guid AppEventId { get; }
    }
}
