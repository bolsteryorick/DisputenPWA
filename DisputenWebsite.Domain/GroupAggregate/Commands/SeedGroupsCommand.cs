using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.GroupAggregate.Commands
{
    public class SeedGroupsCommand : IRequest<SeedGroupsCommandResult>
    {
        public SeedGroupsCommand(
            int nrOfGroups,
            int maxEventsPerGroup,
            int maxMembersPerGroup
            )
        {
            NrOfGroups = nrOfGroups;
            MaxEventsPerGroup = maxEventsPerGroup;
            MaxMembersPerGroup = maxMembersPerGroup;
        }

        public int NrOfGroups { get; }
        public int MaxEventsPerGroup { get; }
        public int MaxMembersPerGroup { get; }
    }
}
