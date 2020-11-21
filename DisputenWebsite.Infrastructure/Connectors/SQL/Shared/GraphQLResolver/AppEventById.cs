using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver
{
    public partial class GraphQLResolver
    {
        public async Task<AppEvent> ResolveAppEventById(
            Guid appEventId,
            AppEventPropertyHelper helper)
        {
            var appEvent = await GetAppEventById(appEventId, helper);
            if (helper.CanGetGroup())
            {
                appEvent.Group = await ResolveGroupById(appEvent.GroupId, helper.GroupPropertyHelper);
            }
            return appEvent;
        }

        private async Task<AppEvent> GetAppEventById(Guid appEventId, AppEventPropertyHelper helper)
        {
            var eventQueryable = QueryableAppEventById(appEventId, helper.LowestEndDate, helper.HighestStartDate);
            return await _eventRepository.GetFirstOrDefault(eventQueryable, helper);
        }

        private IQueryable<DalAppEvent> QueryableAppEventById(Guid appEventId, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _eventRepository.GetQueryable().Where(e => e.Id == appEventId &&
                    e.EndTime > lowestEndDate &&
                        e.StartTime < highestStartDate);
        }
    }
}
