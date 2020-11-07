using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Queries
{
    public class GroupQuery : IRequest<GetGroupQueryResult>
    {
        public Guid GroupId { get; }
        public GroupPropertyHelper GroupPropertyHelper { get; }

        public GroupQuery(
            Guid groupId, 
            GroupPropertyHelper groupPropertyHelper
            )
        {
            GroupId = groupId;
            GroupPropertyHelper = groupPropertyHelper;
        }
    }
}