using AutoMapper;
using AutoMapper.QueryableExtensions;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.Groups
{
    public interface IGroupConnector
    {
        Task<Group> GetGroup(Guid id);
        Task<Group> GetGroup(Guid id, DateTime lowestEndDate, DateTime highestStartDate);
        Task CreateGroup(Group newGroup);
        Task UpdateGroup(Group updatedGroup);
        Task DeleteGroup(Guid id);
    }

    public class GroupConnector : IGroupConnector
    {
        private readonly IRepository<DALGroup> _groupRepository;
        private readonly IRepository<DALAppEvent> _eventRepository;
        private readonly IMapper _mapper;

        public GroupConnector(
            IRepository<DALGroup> groupRepository,
            IRepository<DALAppEvent> eventRepository,
            IMapper mapper
            )
        {
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Group> GetGroup(Guid id)
        {
            return await _groupRepository
                .GetQueryable()
                .ProjectTo<Group>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Group> GetGroup(Guid id, DateTime lowestEndDate, DateTime highestStartDate)
        {
            var group = (await _groupRepository
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.Id == id)).CreateGroup();
            var eventsQueryable = _eventRepository.GetQueryable();
            var events = await eventsQueryable
                .Where(e => e.GroupId == id && 
                    e.EndTime > lowestEndDate &&
                        e.StartTime < highestStartDate)
                .Select(x => x.CreateAppEvent())
                .ToListAsync();
            group.AppEvents = events;
            return group;
        }

        public async Task CreateGroup(Group newGroup)
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
    }
}