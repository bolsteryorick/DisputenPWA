using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.EventAggregate.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IAppEventRepository : IRepository<DALAppEvent>
    {
        Task<AppEvent> GetFirstOrDefault(IQueryable<DALAppEvent> queryable, AppEventPropertyHelper helper);
        Task<List<AppEvent>> GetAll(IQueryable<DALAppEvent> queryable, AppEventPropertyHelper helper);
    }

    public class AppEventRepository : Repository<DALAppEvent>, IAppEventRepository
    {
        public AppEventRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<AppEvent> GetFirstOrDefault(IQueryable<DALAppEvent> queryable, AppEventPropertyHelper helper)
        {
            var selectAppEventQuery = SelectAppEvent(queryable, helper);
            return await selectAppEventQuery.FirstOrDefaultAsync();
        }

        public async Task<List<AppEvent>> GetAll(IQueryable<DALAppEvent> queryable, AppEventPropertyHelper helper)
        {
            var selectAppEventQuery = SelectAppEvent(queryable, helper);
            return await selectAppEventQuery.ToListAsync();
        }

        private IQueryable<AppEvent> SelectAppEvent(IQueryable<DALAppEvent> queryable, AppEventPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new AppEvent
                    {
                        Id = helper.GetId ? x.Id : Guid.Empty,
                        Name = helper.GetName ? x.Name : null,
                        Description = helper.GetDescription ? x.Description : null,
                        StartTime = helper.GetStartTime ? x.StartTime : new DateTime(),
                        EndTime = helper.GetEndTime ? x.EndTime : new DateTime(),
                        GroupId = helper.GetGroupId ? x.GroupId : Guid.Empty,
                    });
        }
    }
}
