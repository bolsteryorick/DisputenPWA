using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Commands
{
    public class CreateAppEventCommand : IRequest<CreateAppEventCommandResult>
    {
        public CreateAppEventCommand(
            string name,
            string description,
            DateTime startTime,
            DateTime endTime,
            Guid groupId
            )
        {
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            GroupId = groupId;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public Guid GroupId { get; }
    }
}
