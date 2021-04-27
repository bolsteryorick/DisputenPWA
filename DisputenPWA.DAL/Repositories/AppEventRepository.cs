using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IAppEventRepository : IRepository<DalAppEvent>
    {
        Task<AppEvent> GetFirstOrDefault(IQueryable<DalAppEvent> queryable, AppEventPropertyHelper helper);
        Task<IList<AppEvent>> GetAll(IQueryable<DalAppEvent> queryable, AppEventPropertyHelper helper);
    }

    public class AppEventRepository : Repository<DalAppEvent>, IAppEventRepository
    {
        public AppEventRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<AppEvent> GetFirstOrDefault(IQueryable<DalAppEvent> queryable, AppEventPropertyHelper helper)
        {
            var selectAppEventQuery = SelectAppEvent(queryable, helper);
            return await selectAppEventQuery.FirstOrDefaultAsync();
        }

        public async Task<IList<AppEvent>> GetAll(IQueryable<DalAppEvent> queryable, AppEventPropertyHelper helper)
        {
            var selectAppEventQuery = SelectAppEvent(queryable, helper);
            return await selectAppEventQuery.ToListAsync();
        }

        private IQueryable<AppEvent> SelectAppEvent(IQueryable<DalAppEvent> queryable, AppEventPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    AppEvent.WithUtcDateKind(
                            x.Id,
                            helper.GetName ? x.Name : null,
                            helper.GetDescription ? x.Description : null,
                            helper.GetStartTime ? x.StartTime : null,
                            helper.GetEndTime ? x.EndTime : null,
                            helper.GetMaxAttendees ? x.MaxAttendees : null,
                            x.GroupId,
                            x.GoogleEventId
                        ));
        }
    }
}
