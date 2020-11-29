using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Groups.GroupById
{
    public class GroupByIdHandler : IRequestHandler<GroupByIdRequest, Group>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IResolveForGroupsService _resolveForGroupsService;

        public GroupByIdHandler(
            IGroupRepository groupRepository,
            IResolveForGroupsService resolveForGroupsService
            )
        {
            _groupRepository = groupRepository;
            _resolveForGroupsService = resolveForGroupsService;
        }

        public async Task<Group> Handle(GroupByIdRequest req, CancellationToken cancellationToken)
        {
            var query = _groupRepository.GetQueryable().Where(x => x.Id == req.Id);
            var groupInList = await _resolveForGroupsService.Resolve(query, req.Helper, cancellationToken);
            return groupInList.FirstOrDefault();
        }
    }
}
