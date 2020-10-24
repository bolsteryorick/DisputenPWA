using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Queries
{
    public class GetGroupQuery : IRequest<GetGroupQueryResult>
    {
        public Guid GroupId { get; }
        public DateTime? LowestEndDate { get; }
        public DateTime? HighestStartDate { get; }

        public GetGroupQuery(
            Guid groupId, 
            DateTime? lowestEndDate, 
            DateTime? highestStartDate
            )
        {
            GroupId = groupId;
            LowestEndDate = lowestEndDate;
            HighestStartDate = highestStartDate;
        }
    }
}