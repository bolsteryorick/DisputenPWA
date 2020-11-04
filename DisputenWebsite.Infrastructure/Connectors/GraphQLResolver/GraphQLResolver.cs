using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.GraphQLResolver
{
    public interface IGraphQLResolver
    {
        Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper);
        Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(
            IEnumerable<Guid> groupIds,
            AppEventPropertyHelper helper);
        Task<AppEvent> ResolveAppEvent(
            Guid appEventId,
            AppEventPropertyHelper helper);
    }

    public class GraphQLResolver : IGraphQLResolver
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppEventRepository _eventRepository;

        public GraphQLResolver(
            IGroupRepository groupRepository,
            IAppEventRepository eventRepository
            )
        {
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
        }


        // (for this build another service so you can switch and see which one is faster)Mabye instead of multiple calls you could chain joins? https://entityframework.net/joining Also see card for more links.
        // maybe use strategy? or just split into services, but strategy could be cool.
        public async Task<Group> ResolveGroup(Guid id, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => x.Id == id);
            var group = await _groupRepository.GetFirstOrDefault(queryable, helper);
            if (helper.CanGetAppEvents())
            {
                var appEvents = await ResolveAppEvents(new List<Guid> { id }, helper.AppEventPropertyHelper);
                group.AppEvents = appEvents;
            }
            return group;
        }

        public async Task<IReadOnlyCollection<Group>> ResolveGroups(IEnumerable<Guid> groupIds, GroupPropertyHelper helper)
        {
            var queryable = _groupRepository.GetQueryable().Where(x => groupIds.Contains(x.Id));
            var groups = await _groupRepository.GetAll(queryable, helper);
            if (helper.CanGetAppEvents())
            {
                var appEvents = await ResolveAppEvents(groupIds, helper.AppEventPropertyHelper);
                foreach(var group in groups)
                {
                    group.AppEvents = appEvents.Where(x => x.GroupId == group.Id).ToList();
                }
            }
            return groups;
        }

        public async Task<IReadOnlyCollection<AppEvent>> ResolveAppEvents(
            IEnumerable<Guid> groupIds,
            AppEventPropertyHelper helper)
        {
            var eventQueryable = _eventRepository.GetQueryable().Where(e => groupIds.Contains(e.GroupId) &&
                    e.EndTime > EventRange.LowestEndDate &&
                        e.StartTime < EventRange.HighestStartDate);
            var events = await _eventRepository.GetAll(eventQueryable, helper);
            if (helper.CanGetGroup())
            {
                var groups = await ResolveGroups(groupIds, helper.GroupPropertyHelper);
                foreach(var appEvent in events)
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
                    e.EndTime > EventRange.LowestEndDate &&
                        e.StartTime < EventRange.HighestStartDate);
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
