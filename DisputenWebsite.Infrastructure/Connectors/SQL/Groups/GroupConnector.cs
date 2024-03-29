﻿using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.SQLResolver.Groups.GroupById;
using MediatR;
using System;
using System.Collections.Generic;
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
        private readonly IMediator _mediator;

        public GroupConnector(
            IGroupRepository groupRepository,
            IMediator mediator
            )
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task<Group> GetGroup(Guid id, GroupPropertyHelper helper)
        {
            return await _mediator.Send(new GroupByIdRequest(id, helper));
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