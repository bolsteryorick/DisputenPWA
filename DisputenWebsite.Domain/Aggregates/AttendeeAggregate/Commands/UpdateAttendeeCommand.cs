using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands
{
    public class UpdateAttendeeCommand : IRequest<UpdateAttendeeCommandResult>
    {
        public UpdateAttendeeCommand(
            Guid atttendeeId,
            bool? paid
            )
        {
            AtttendeeId = atttendeeId;
            Paid = paid;
        }

        public Guid AtttendeeId { get; }
        public bool? Paid { get; }
    }
}
