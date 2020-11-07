using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared
{
    public partial class GraphQLResolver
    {
        public async Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(
           IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper)
        {
            var eventQueryable = _eventRepository.GetQueryable().Where(e => groupIds.Contains(e.GroupId) &&
                    e.EndTime > helper.LowestEndDate &&
                        e.StartTime < helper.HighestStartDate);
            var events = await _eventRepository.GetAll(eventQueryable, helper);
            if (helper.CanGetGroup())
            {
                var groups = await ResolveGroups(groupIds, helper.GroupPropertyHelper);
                foreach (var appEvent in events)
                {
                    appEvent.Group = groups.FirstOrDefault(x => x.Id == appEvent.GroupId);
                }
            }
            return events;
        }

        public async Task<AppEvent> ResolveAppEvent(
            Guid appEventId,
            AppEventPropertyHelper helper)
        {
            var eventQueryable = _eventRepository.GetQueryable().Where(e => e.Id == appEventId &&
                    e.EndTime > helper.LowestEndDate &&
                        e.StartTime < helper.HighestStartDate);
            var appEvent = await _eventRepository.GetFirstOrDefault(eventQueryable, helper);
            if (helper.CanGetGroup())
            {
                var group = await ResolveGroup(appEvent.GroupId, helper.GroupPropertyHelper);
                appEvent.Group = group;
            }
            return appEvent;
        }
    }
}
