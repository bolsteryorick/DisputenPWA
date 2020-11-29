using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Groups.GroupsByIds
{
    public class GroupsByIdsHandler : IRequestHandler<GroupsByIdsRequest, IReadOnlyCollection<Group>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IResolveForGroupsService _resolveForGroupsService;

        public GroupsByIdsHandler(
            IGroupRepository groupRepository,
            IResolveForGroupsService resolveForGroupsService
            )
        {
            _groupRepository = groupRepository;
            _resolveForGroupsService = resolveForGroupsService;
        }

        public async Task<IReadOnlyCollection<Group>> Handle(GroupsByIdsRequest req, CancellationToken cancellationToken)
        {
            var query = _groupRepository.GetQueryable().Where(x => req.GroupIds.Contains(x.Id));
            return await _resolveForGroupsService.Resolve(query, req.Helper, cancellationToken);
        }
    }
}
