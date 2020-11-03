using AutoMapper;
using AutoMapper.QueryableExtensions;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.AppEvents
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

        public AppEventConnector(
            IAppEventRepository appEventRepository
            )
        {
            _appEventRepository = appEventRepository;
        }

        public async Task<AppEvent> GetAppEvent(Guid id, AppEventPropertyHelper helper)
        {
            // zie stappen.txt
            var queryable = _appEventRepository.GetQueryable().Where(x => x.Id == id);
            return await _appEventRepository.GetFirstOrDefault(queryable, helper);
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
            var dalGroup = new DALAppEvent { Id = id };
            var appEvent = await _appEventRepository.UpdateProperties(dalGroup, properties);
            return appEvent.CreateAppEvent();
        }
    }
}
