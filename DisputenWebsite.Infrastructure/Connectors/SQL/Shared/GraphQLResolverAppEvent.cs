using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.EventAggregate.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(
           IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper)
        {
            var events = await GetAppEvents(groupIds, helper);
            if (helper.CanGetGroup())
            {
                events = await AddGroupsToAppEvents(events, groupIds, helper);
            }
            return events.ToImmutableList();
        }

        public async Task<AppEvent> ResolveAppEvent(
            Guid appEventId,
            AppEventPropertyHelper helper)
        {
            var appEvent = await GetAppEvent(appEventId, helper);
            if (helper.CanGetGroup())
            {
                appEvent.Group = await ResolveGroup(appEvent.GroupId, helper.GroupPropertyHelper);
            }
            return appEvent;
        }

        private async Task<AppEvent> GetAppEvent(Guid appEventId, AppEventPropertyHelper helper)
        {
            var eventQueryable = AppEventQueryable(appEventId, helper.LowestEndDate, helper.HighestStartDate);
            return await _eventRepository.GetFirstOrDefault(eventQueryable, helper);
        }

        private IQueryable<DALAppEvent> AppEventQueryable(Guid appEventId, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _eventRepository.GetQueryable().Where(e => e.Id == appEventId &&
                    e.EndTime > lowestEndDate &&
                        e.StartTime < highestStartDate);
        }

        private async Task<IList<AppEvent>> GetAppEvents(IEnumerable<Guid> groupIds, AppEventPropertyHelper helper)
        {
            var eventsQueryable = AppEventsQueryable(groupIds, helper.LowestEndDate, helper.HighestStartDate);
            return await _eventRepository.GetAll(eventsQueryable, helper);
        }

        private IQueryable<DALAppEvent> AppEventsQueryable(IEnumerable<Guid> groupIds, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _eventRepository
                .GetQueryable()
                .Where(
                    e => groupIds.Contains(e.GroupId) &&
                    e.EndTime > lowestEndDate &&
                    e.StartTime < highestStartDate
                );
        }

        private async Task<IList<AppEvent>> AddGroupsToAppEvents(IList<AppEvent> events, IEnumerable<Guid> groupIds, AppEventPropertyHelper helper)
        {
            var groups = await ResolveGroups(groupIds, helper.GroupPropertyHelper);
            foreach (var appEvent in events)
            {
                appEvent.Group = groups.FirstOrDefault(x => x.Id == appEvent.GroupId);
            }
            return events;
        }
    }
}
