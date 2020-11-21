using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Handlers
{
    public class AppEventsFromGroupIdsHandler : IRequestHandler<AppEventsFromGroupIdsRequest, AppEventsFromGroupIdsResult>
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IMediator _mediator;

        public AppEventsFromGroupIdsHandler(
            IAppEventRepository appEventRepository,
            IMediator mediator
            )
        {
            _appEventRepository = appEventRepository;
            _mediator = mediator;
        }

        public async Task<AppEventsFromGroupIdsResult> Handle(AppEventsFromGroupIdsRequest req, CancellationToken cancellationToken)
        {
            return new AppEventsFromGroupIdsResult(
                await ResolveAppEventsFromGroupIds(req.GroupIds, req.Helper, cancellationToken)
                );
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
                events = await GetGroupsForAppEvents(events, groupIds, helper, cancellationToken);
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

        private async Task<IList<AppEvent>> GetGroupsForAppEvents(IList<AppEvent> events, IEnumerable<Guid> groupIds, AppEventPropertyHelper helper, CancellationToken cancellationToken)
        {
            var groups = (await _mediator.Send(new GroupsByIdsRequest(groupIds, helper.GroupPropertyHelper), cancellationToken)).Result;
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var appEvent in events)
            {
                if (groupsDictionary.TryGetValue(appEvent.GroupId, out var group)) appEvent.Group = group;
            }
            return events;
        }
    }
}
