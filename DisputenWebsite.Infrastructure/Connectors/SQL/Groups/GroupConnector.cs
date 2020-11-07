using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Groups
{
    public interface IGroupConnector
    {
        Task<Group> GetGroup(Guid groupId, GroupPropertyHelper helper);
        Task Create(Group newGroup);
        Task UpdateGroup(Group updatedGroup);
        Task DeleteGroup(Guid id);
        Task Create(List<DALGroup> dalGroups);
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

        public async Task<Group> GetGroup(Guid id,
            GroupPropertyHelper helper)
        {
            return await _graphQLResolver.ResolveGroup(id, helper);
        }

        public async Task Create(Group newGroup)
        {
            await _groupRepository
                .Add(newGroup.CreateDALGroup());
        }

        public async Task UpdateGroup(Group updatedGroup)
        {
            await _groupRepository
                .Update(updatedGroup.CreateDALGroup());
        }

        public async Task DeleteGroup(Guid id)
        {
            await _groupRepository.DeleteById(id);
        }

        public async Task Create(List<DALGroup> dalGroups)
        {
            await _groupRepository.Add(dalGroups);
        }

        public async Task<Group> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var dalGroup = new DALGroup { Id = id };
            var group = await _groupRepository.UpdateProperties(dalGroup, properties);
            return group.CreateGroup();
        }
    }
}