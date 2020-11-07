﻿using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Queries;
using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Queries
{
    public class GetGroupHandler : IRequestHandler<GroupQuery, GetGroupQueryResult>
    {
        private readonly IGroupConnector _groupConnector;

        public GetGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<GetGroupQueryResult> Handle(GroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupConnector.GetGroup(request.GroupId, request.GroupPropertyHelper);
            return new GetGroupQueryResult(group);
        }
    }
}