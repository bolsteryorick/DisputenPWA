using AutoMapper;
using AutoMapper.QueryableExtensions;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DALObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.AppEvents
{
    public interface IAppEventConnector
    {
        Task<AppEvent> GetAppEvent(Guid id);
        Task CreateAppEvent(AppEvent newAppEvent);
        Task UpdateAppEvent(AppEvent updatedAppEvent);
        Task DeleteAppEvent(Guid id);
    }

    public class AppEventConnector : IAppEventConnector
    {
        private readonly IRepository<DALAppEvent> _appEventRepository;
        private readonly IMapper _mapper;

        public AppEventConnector(
            IRepository<DALAppEvent> appEventRepository,
            IMapper mapper
            )
        {
            _appEventRepository = appEventRepository;
            _mapper = mapper;
        }

        public Task<AppEvent> GetAppEvent(Guid id)
        {
            return _appEventRepository
                .GetQueryable()
                .ProjectTo<AppEvent>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAppEvent(AppEvent newAppEvent)
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
    }
}
