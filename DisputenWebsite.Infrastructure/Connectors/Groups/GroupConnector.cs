using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.Groups
{
    public interface IGroupConnector
    {
        Task<Group> GetGroup(Guid id);
        Task CreateGroup(Group newGroup);
        Task UpdateGroup(Group updatedGroup);
        Task DeleteGroup(Guid id);
    }

    public class GroupConnector : IGroupConnector
    {
        private readonly IRepository<Group> _groupRepository;

        public GroupConnector(
            IRepository<Group> groupRepository
            )
        {
            _groupRepository = groupRepository;
        }

        public Task<Group> GetGroup(Guid id)
        {
            return _groupRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateGroup(Group newGroup)
        {
            await _groupRepository.Add(newGroup);
        }

        public async Task UpdateGroup(Group updatedGroup)
        {
            await _groupRepository.Update(updatedGroup);
        }

        public async Task DeleteGroup(Guid id)
        {
            await _groupRepository.DeleteById(id);
        }
    }
}