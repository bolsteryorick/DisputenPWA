using DisputenPWA.Domain.GroupAggregate.Commands;
using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Commands
{
    public class SeedGroupsHandler : IRequestHandler<SeedGroupsCommand, SeedGroupsCommandResult>
    {
        private readonly ISeedingService _seedingService;

        public SeedGroupsHandler(
            ISeedingService seedingService
            )
        {
            _seedingService = seedingService;
        }
        public async Task<SeedGroupsCommandResult> Handle(SeedGroupsCommand request, CancellationToken cancellationToken)
        {
            await _seedingService.Seed(request.NrOfGroups, request.MaxEventsPerGroup);
            return new SeedGroupsCommandResult(null);
        }
    }
}
