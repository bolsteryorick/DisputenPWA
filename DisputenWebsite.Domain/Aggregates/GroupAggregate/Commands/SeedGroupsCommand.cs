using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Commands
{
    public class SeedGroupsCommand : IRequest<SeedGroupsCommandResult>
    {
        public SeedGroupsCommand(
            int nrOfGroups,
            int maxEventsPerGroup,
            int maxMembersPerGroup,
            int maxAttendeesPerEvent
            )
        {
            NrOfGroups = nrOfGroups;
            MaxEventsPerGroup = maxEventsPerGroup;
            MaxMembersPerGroup = maxMembersPerGroup;
            MaxAttendeesPerEvent = maxAttendeesPerEvent;
        }

        public int NrOfGroups { get; }
        public int MaxEventsPerGroup { get; }
        public int MaxMembersPerGroup { get; }
        public int MaxAttendeesPerEvent { get; }
    }
}
