using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DALObject;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IGroupRepository : IRepository<DALGroup>
    {
        Task<Group> GetFirstOrDefault(IQueryable<DALGroup> queryable, GroupPropertyHelper helper);
        Task<List<Group>> GetAll(IQueryable<DALGroup> queryable, GroupPropertyHelper helper);
    }

    public class GroupRepository : Repository<DALGroup>, IGroupRepository
    {
        public GroupRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<Group> GetGroup(Guid id, GroupPropertyHelper helper)
        {
            return await GetQueryable()
                .Where(x => x.Id == id)
                .Select(x =>
                    new Group
                    {
                        Id = x.Id,
                        Name = helper.GetName ? x.Name : null,
                        Description = helper.GetDescription ? x.Description : null,
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<Group> GetFirstOrDefault(IQueryable<DALGroup> queryable, GroupPropertyHelper helper)
        {
            var selectAppEventQuery = SelectGroup(queryable, helper);
            return await selectAppEventQuery.FirstOrDefaultAsync();
        }

        public async Task<List<Group>> GetAll(IQueryable<DALGroup> queryable, GroupPropertyHelper helper)
        {
            var selectAppEventQuery = SelectGroup(queryable, helper);
            return await selectAppEventQuery.ToListAsync();
        }

        private IQueryable<Group> SelectGroup(IQueryable<DALGroup> queryable, GroupPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new Group
                    {
                        Id = x.Id,
                        Name = helper.GetName ? x.Name : null,
                        Description = helper.GetDescription ? x.Description : null,
                    });
        }
    }
}
