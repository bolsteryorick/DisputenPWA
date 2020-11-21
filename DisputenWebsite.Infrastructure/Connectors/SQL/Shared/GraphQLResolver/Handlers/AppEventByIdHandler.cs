using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Handlers
{
    public class AppEventByIdHandler : IRequestHandler<AppEventByIdRequest, AppEventByIdResult>
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IMediator _mediator;

        public AppEventByIdHandler(
            IAppEventRepository appEventRepository,
            IMediator mediator
            )
        {
            _appEventRepository = appEventRepository;
            _mediator = mediator;
        }

        public async Task<AppEventByIdResult> Handle(AppEventByIdRequest req, CancellationToken cancellationToken)
        {
            return new AppEventByIdResult(
                await ResolveAppEventById(req.EventId, req.Helper, cancellationToken)
                );
        }

        private async Task<AppEvent> ResolveAppEventById(
            Guid appEventId,
            AppEventPropertyHelper helper,
            CancellationToken cancellationToken)
        {
            var appEvent = await GetAppEventById(appEventId, helper);
            if (helper.CanGetGroup())
            {
                appEvent.Group = (await _mediator.Send(new GroupByIdRequest(appEvent.GroupId, helper.GroupPropertyHelper), cancellationToken)).Result;
            }
            return appEvent;
        }

        private async Task<AppEvent> GetAppEventById(Guid appEventId, AppEventPropertyHelper helper)
        {
            var eventQueryable = QueryableAppEventById(appEventId, helper.LowestEndDate, helper.HighestStartDate);
            return await _appEventRepository.GetFirstOrDefault(eventQueryable, helper);
        }

        private IQueryable<DalAppEvent> QueryableAppEventById(Guid appEventId, DateTime lowestEndDate, DateTime highestStartDate)
        {
            return _appEventRepository.GetQueryable().Where(e => e.Id == appEventId &&
                    e.EndTime > lowestEndDate &&
                        e.StartTime < highestStartDate);
        }
    }
}