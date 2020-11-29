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
            var query = QueryableAppEventsByGroupIds(req.GroupIds, req.Helper.LowestEndDate, req.Helper.HighestStartDate);
            return await _resolveForAppEventsService.Resolve(query, req.Helper, cancellationToken);
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
