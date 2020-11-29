using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Commands
{
    public class UpdateAppEventCommand : IRequest<UpdateAppEventCommandResult>
    {
        public UpdateAppEventCommand(
            Guid id,
            string name,
            string description,
            DateTime? startTime,
            DateTime? endTime,
            int? maxAttendees
            )
        {
            Id = id;
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            MaxAttendees = maxAttendees;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }
        public int? MaxAttendees { get; }
    }
}
