using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
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
        private readonly IResolveForAppEventsService _resolveForAppEventsService;

        public AppEventByIdHandler(
            IAppEventRepository appEventRepository,
            IResolveForAppEventsService resolveForAppEventsService
            )
        {
            _appEventRepository = appEventRepository;
            _resolveForAppEventsService = resolveForAppEventsService;
        }

        public async Task<AppEvent> Handle(AppEventByIdRequest req, CancellationToken cancellationToken)
        {
            var query = QueryableAppEventById(req.EventId);
            var appEventInList = await _resolveForAppEventsService.Resolve(query, req.Helper, cancellationToken);
            return appEventInList.FirstOrDefault();
        }

        private IQueryable<DalAppEvent> QueryableAppEventById(Guid appEventId)
        {
            return _appEventRepository.GetQueryable().Where(e => e.Id == appEventId);
        }
    }
}