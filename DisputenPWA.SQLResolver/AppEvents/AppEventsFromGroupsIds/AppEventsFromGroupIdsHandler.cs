using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventsFromGroupsIds
{
    public class AppEventsFromGroupIdsHandler : IRequestHandler<AppEventsFromGroupIdsRequest, IReadOnlyCollection<AppEvent>>
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IResolveForAppEventsService _resolveForAppEventsService;

        public AppEventsFromGroupIdsHandler(
            IAppEventRepository appEventRepository,
            IResolveForAppEventsService resolveForAppEventsService
            )
        {
            _appEventRepository = appEventRepository;
            _resolveForAppEventsService = resolveForAppEventsService;
        }

        public async Task<IReadOnlyCollection<AppEvent>> Handle(AppEventsFromGroupIdsRequest req, CancellationToken cancellationToken)
        {
            return await ResolveAppEventsFromGroupIds(req.GroupIds, req.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<AppEvent>> ResolveAppEventsFromGroupIds(
           IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper,
           CancellationToken cancellationToken
            )
        {
            var events = await GetAppEventsFromGroupIds(groupIds, helper);
            if (helper.CanGetGroup())
            {
                events = await _resolveForAppEventsService.GetGroupsForAppEvents(events, groupIds, helper, cancellationToken);
            }
            if (helper.CanGetAttendees())
            {
                var appEventIds = events.Select(x => x.Id);
                events = await _resolveForAppEventsService.GetAttendeesForAppEvents(events, appEventIds, helper, cancellationToken);
            }
            return events.ToImmutableList();
        }

        private async Task<IList<AppEvent>> GetAppEventsFromGroupIds(IEnumerable<Guid> groupIds, AppEventPropertyHelper helper)
        {
            var eventsQueryable = QueryableAppEventsByGroupIds(groupIds, helper.LowestEndDate, helper.HighestStartDate);
            return await _appEventRepository.GetAll(eventsQueryable, helper);
        }

        private IQueryable<DalAppEvent> QueryableAppEventsByGroupIds(IEnumerable<Guid> groupIds, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _appEventRepository
                .GetQueryable()
                .Where(
                    e => groupIds.Contains(e.GroupId) &&
                    e.EndTime > lowestEndDate &&
                    e.StartTime < highestStartDate
                );
        }
    }
}
