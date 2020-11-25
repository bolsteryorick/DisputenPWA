using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventsByIds
{
    public class AppEventsByIdsHandler : IRequestHandler<AppEventsByIdsRequest, IReadOnlyCollection<AppEvent>>
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IResolveForAppEventsService _resolveForAppEventsService;

        public AppEventsByIdsHandler(
            IAppEventRepository appEventRepository,
            IResolveForAppEventsService resolveForAppEventsService
            )
        {
            _appEventRepository = appEventRepository;
            _resolveForAppEventsService = resolveForAppEventsService;
        }

        public async Task<IReadOnlyCollection<AppEvent>> Handle(AppEventsByIdsRequest req, CancellationToken cancellationToken)
        {
            return await ResolveAppEventsByIds(req.AppEventIds, req.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<AppEvent>> ResolveAppEventsByIds(
           IEnumerable<Guid> appEventIds,
           AppEventPropertyHelper helper,
           CancellationToken cancellationToken
            )
        {
            var events = await GetAppEventsByIds(appEventIds, helper);
            if (helper.CanGetGroup())
            {
                var groupIds = events.Select(x => x.GroupId).Distinct();
                events = await _resolveForAppEventsService.GetGroupsForAppEvents(events, groupIds, helper, cancellationToken);
            }
            if (helper.CanGetAttendees())
            {
                events = await _resolveForAppEventsService.GetAttendeesForAppEvents(events, appEventIds, helper, cancellationToken);
            }
            return events.ToImmutableList();
        }

        private async Task<IList<AppEvent>> GetAppEventsByIds(IEnumerable<Guid> appEventIds, AppEventPropertyHelper helper)
        {
            var eventsQueryable = QueryableAppEventsByIds(appEventIds, helper.LowestEndDate, helper.HighestStartDate);
            return await _appEventRepository.GetAll(eventsQueryable, helper);
        }

        private IQueryable<DalAppEvent> QueryableAppEventsByIds(IEnumerable<Guid> appeventIds, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _appEventRepository
                .GetQueryable()
                .Where(
                    e => appeventIds.Contains(e.Id) &&
                    e.EndTime > lowestEndDate &&
                    e.StartTime < highestStartDate
                );
        }
    }
}
