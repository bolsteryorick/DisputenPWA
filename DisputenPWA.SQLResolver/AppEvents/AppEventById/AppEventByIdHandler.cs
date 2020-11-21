using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.SQLResolver.Groups.GroupById;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventById
{
    public class AppEventByIdHandler : IRequestHandler<AppEventByIdRequest, AppEvent>
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

        public async Task<AppEvent> Handle(AppEventByIdRequest req, CancellationToken cancellationToken)
        {
            return await ResolveAppEventById(req.EventId, req.Helper, cancellationToken);
        }

        private async Task<AppEvent> ResolveAppEventById(
            Guid appEventId,
            AppEventPropertyHelper helper,
            CancellationToken cancellationToken)
        {
            var appEvent = await GetAppEventById(appEventId, helper);
            if (helper.CanGetGroup())
            {
                appEvent.Group = await _mediator.Send(new GroupByIdRequest(appEvent.GroupId, helper.GroupPropertyHelper), cancellationToken);
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