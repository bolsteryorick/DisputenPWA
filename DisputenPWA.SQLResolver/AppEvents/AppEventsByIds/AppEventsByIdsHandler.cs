using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
            var query = QueryableAppEventsByIds(req.AppEventIds, req.Helper.LowestEndDate, req.Helper.HighestStartDate);
            return await _resolveForAppEventsService.Resolve(query, req.Helper, cancellationToken);
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
