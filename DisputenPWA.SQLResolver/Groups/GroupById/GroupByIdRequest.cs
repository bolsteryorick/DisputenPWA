using DisputenPWA.Domain.Aggregates.GroupAggregate;
using MediatR;
using System;

namespace DisputenPWA.SQLResolver.Groups.GroupById
{
    public class GroupByIdRequest : IRequest<Group>
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
