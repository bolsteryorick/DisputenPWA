using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DalObject;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Groups
{
    public interface IGroupConnector
    {
        Task<Group> GetGroup(Guid groupId, GroupPropertyHelper helper);
        Task Create(Group newGroup);
        Task Delete(Guid id);
        Task Create(IEnumerable<DalGroup> dalGroups);
        Task<Group> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class GroupConnector : IGroupConnector
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGraphQLResolver _graphQLResolver;

        public GroupConnector(
            IGroupRepository groupRepository,
            IGraphQLResolver graphQLResolver
            )
        {
            _groupRepository = groupRepository;
            _graphQLResolver = graphQLResolver;
        }

        public async Task<Group> GetGroup(Guid id, GroupPropertyHelper helper)
        {
            return await _graphQLResolver.ResolveGroup(id, helper);
        }

        public async Task Create(Group group)
        {
            await _groupRepository.Add(group.CreateDalGroup());
        }

        public async Task Delete(Guid id)
        {
            await _groupRepository.DeleteByObject(new DalGroup { Id = id });
        }

        public async Task Create(IEnumerable<DalGroup> dalGroups)
        {
            await _groupRepository.Add(dalGroups);
        }

        public async Task<Group> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var group = await _groupRepository
                .UpdateProperties(new DalGroup { Id = id }, properties);
            return group.CreateGroup();
        }
    }
}