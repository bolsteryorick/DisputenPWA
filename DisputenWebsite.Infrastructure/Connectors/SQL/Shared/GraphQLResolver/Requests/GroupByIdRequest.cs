using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class GroupByIdRequest : IRequest<GroupByIdResult>
    {
        public GroupByIdRequest(
            Guid id, 
            GroupPropertyHelper helper
            )
        {
            Id = id;
            Helper = helper;
        }

        public Guid Id { get; }
        public GroupPropertyHelper Helper { get; }
    }
}
