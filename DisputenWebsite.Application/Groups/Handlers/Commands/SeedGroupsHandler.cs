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
        public async Task<SeedGroupsCommandResult> Handle(SeedGroupsCommand req, CancellationToken cancellationToken)
        {
            await _seedingService.Seed(
                req.NrOfGroups,
                req.MaxEventsPerGroup,
                req.MaxMembersPerGroup,
                req.MaxAttendeesPerEvent
                );
            return new SeedGroupsCommandResult(null);
        }
    }
}