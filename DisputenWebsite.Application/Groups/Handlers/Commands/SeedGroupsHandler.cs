using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using DisputenPWA.Infrastructure;
using MediatR;
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
            await _seedingService.Seed(request.NrOfGroups, request.MaxEventsPerGroup, request.MaxMembersPerGroup);
            return new SeedGroupsCommandResult(null);
        }
    }
}
