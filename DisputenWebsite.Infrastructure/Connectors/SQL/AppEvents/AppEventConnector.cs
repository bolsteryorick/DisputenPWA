using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.AppEvents
{
    public interface IAppEventConnector
    {
        Task<AppEvent> GetAppEvent(Guid id, AppEventPropertyHelper helper);
        Task Create(AppEvent newAppEvent);
        Task UpdateAppEvent(AppEvent updatedAppEvent);
        Task DeleteAppEvent(Guid id);
        Task Create(List<DALAppEvent> dalAppEvents);
        Task<AppEvent> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class AppEventConnector : IAppEventConnector
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IGraphQLResolver _graphQLResolver;

        public AppEventConnector(
            IAppEventRepository appEventRepository,
            IGraphQLResolver graphQLResolver
            )
        {
            _appEventRepository = appEventRepository;
            _graphQLResolver = graphQLResolver;
        }

        public async Task<AppEvent> GetAppEvent(Guid id, AppEventPropertyHelper helper)
        {
            return await _graphQLResolver.ResolveAppEvent(id, helper);
        }

        public async Task Create(AppEvent newAppEvent)
        {
            await _appEventRepository
                .Add(newAppEvent.CreateDALAppEvent());
        }

        public async Task UpdateAppEvent(AppEvent updatedAppEvent)
        {
            await _appEventRepository
                .Update(updatedAppEvent.CreateDALAppEvent());
        }

        public async Task DeleteAppEvent(Guid id)
        {
            await _appEventRepository.DeleteById(id);
        }

        public async Task Create(List<DALAppEvent> dalAppEvents)
        {
            await _appEventRepository.Add(dalAppEvents);
        }

        public async Task<AppEvent> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var appEvent = await _appEventRepository
                .UpdateProperties(new DALAppEvent { Id = id }, properties);
            return appEvent.CreateAppEvent();
        }
    }
}
