using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers;
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
        public GroupPropertyHelper GroupPropertyHelper { get; }

        public GetGroupQuery(
            Guid groupId, 
            DateTime? lowestEndDate, 
            DateTime? highestStartDate,
            GroupPropertyHelper groupPropertyHelper
            )
        {
            GroupId = groupId;
            LowestEndDate = lowestEndDate ?? EventRange.LowestEndDate;
            HighestStartDate = highestStartDate ?? EventRange.HighestStartDate;
            GroupPropertyHelper = groupPropertyHelper;
        }
    }
}