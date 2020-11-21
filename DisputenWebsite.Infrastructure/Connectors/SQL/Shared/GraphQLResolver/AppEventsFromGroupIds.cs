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
        public async Task<IReadOnlyCollection<AppEvent>> ResolveAppEventsFromGroupIds(
           IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper)
        {
            var events = await GetAppEventsFromGroupIds(groupIds, helper);
            if (helper.CanGetGroup())
            {
                events = await GetGroupsForAppEvents(events, groupIds, helper);
            }
            return events.ToImmutableList();
        }

        private async Task<IList<AppEvent>> GetAppEventsFromGroupIds(IEnumerable<Guid> groupIds, AppEventPropertyHelper helper)
        {
            var eventsQueryable = QueryableAppEventsByGroupIds(groupIds, helper.LowestEndDate, helper.HighestStartDate);
            return await _eventRepository.GetAll(eventsQueryable, helper);
        }

        private IQueryable<DalAppEvent> QueryableAppEventsByGroupIds(IEnumerable<Guid> groupIds, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _eventRepository
                .GetQueryable()
                .Where(
                    e => groupIds.Contains(e.GroupId) &&
                    e.EndTime > lowestEndDate &&
                    e.StartTime < highestStartDate
                );
        }

        private async Task<IList<AppEvent>> GetGroupsForAppEvents(IList<AppEvent> events, IEnumerable<Guid> groupIds, AppEventPropertyHelper helper)
        {
            var groups = await ResolveGroupsByIds(groupIds, helper.GroupPropertyHelper);
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var appEvent in events)
            {
                if (groupsDictionary.TryGetValue(appEvent.GroupId, out var group)) appEvent.Group = group;
            }
            return events;
        }
    }
}
