using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.DalObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IGroupRepository : IRepository<DalGroup>
    {
        Task<Group> GetFirstOrDefault(IQueryable<DalGroup> queryable, GroupPropertyHelper helper);
        Task<List<Group>> GetAll(IQueryable<DalGroup> queryable, GroupPropertyHelper helper);
    }

    public class GroupRepository : Repository<DalGroup>, IGroupRepository
    {
        public GroupRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<Group> GetFirstOrDefault(IQueryable<DalGroup> queryable, GroupPropertyHelper helper)
        {
            var selectAppEventQuery = SelectGroup(queryable, helper);
            return await selectAppEventQuery.FirstOrDefaultAsync();
        }

        public async Task<List<Group>> GetAll(IQueryable<DalGroup> queryable, GroupPropertyHelper helper)
        {
            var selectAppEventQuery = SelectGroup(queryable, helper);
            return await selectAppEventQuery.ToListAsync();
        }

        private IQueryable<Group> SelectGroup(IQueryable<DalGroup> queryable, GroupPropertyHelper helper)
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
