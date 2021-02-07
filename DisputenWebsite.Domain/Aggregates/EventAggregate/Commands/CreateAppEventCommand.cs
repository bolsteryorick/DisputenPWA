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
            int? maxAttendees,
            Guid groupId
            )
        {
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            Maxattendees = maxAttendees;
            GroupId = groupId;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public int? Maxattendees { get; }
        public Guid GroupId { get; }
    }
}
