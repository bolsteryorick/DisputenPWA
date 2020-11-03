using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Commands
{
    public class SeedGroupsCommand : IRequest<SeedGroupsCommandResult>
    {
        public SeedGroupsCommand(
            int nrOfGroups,
            int maxEventsPerGroup
            )
        {
            NrOfGroups = nrOfGroups;
            MaxEventsPerGroup = maxEventsPerGroup;
        }

        public int NrOfGroups { get; }
        public int MaxEventsPerGroup { get; }
    }
}
