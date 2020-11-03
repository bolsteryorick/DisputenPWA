using DisputenPWA.Domain.EventAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Commands
{
    public class UpdateAppEventCommand : IRequest<UpdateAppEventCommandResult>
    {
        public UpdateAppEventCommand(
            Guid id,
            string name,
            string description,
            DateTime startTime,
            DateTime endTime
            )
        {
            Id = id;
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
    }
}
