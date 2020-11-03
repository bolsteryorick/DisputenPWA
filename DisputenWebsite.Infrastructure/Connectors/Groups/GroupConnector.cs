using AutoMapper;
using AutoMapper.QueryableExtensions;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.Groups
{
    public interface IGroupConnector
    {
        //Task<Group> GetGroup(Guid id);
        //Task<Group> GetGroup(Guid id, DateTime lowestEndDate, DateTime highestStartDate);
        Task<Group> GetGroup(Guid groupId, DateTime lowestEndDate,
            DateTime highestStartDate, GroupPropertyHelper helper);
        Task Create(Group newGroup);
        Task UpdateGroup(Group updatedGroup);
        Task DeleteGroup(Guid id);
        Task Create(List<DALGroup> dalGroups);
        Task<Group> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class GroupConnector : IGroupConnector
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppEventRepository _eventRepository;

        public GroupConnector(
            IGroupRepository groupRepository,
            IAppEventRepository eventRepository
            )
        {
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Group> GetGroup(Guid id, 
            DateTime lowestEndDate, 
            DateTime highestStartDate, 
            GroupPropertyHelper helper)
        {
            // zie stappen.txt
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            var group = await _groupRepository.GetFirstOrDefault(queryable, helper);
            if (helper.GetAppEvents)
            {
                var eventQueryable = _eventRepository.GetQueryable().Where(e => e.GroupId == id &&
                    e.EndTime > lowestEndDate &&
                        e.StartTime < highestStartDate);
                var events = await _eventRepository.GetAll(eventQueryable, helper.AppEventPropertyHelper);
                group.AppEvents = events;
            }
            
            return group;
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