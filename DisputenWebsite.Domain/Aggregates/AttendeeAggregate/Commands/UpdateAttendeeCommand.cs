using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands
{
    public class UpdateAttendeeCommand : IRequest<UpdateAttendeeCommandResult>
    {
        public UpdateAttendeeCommand(
            Guid id,
            bool? paid
            )
        {
            Id = id;
            Paid = paid;
        }

        public Guid Id { get; }
        public bool? Paid { get; }
    }
}
