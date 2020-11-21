﻿using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.AppEvents
{
    public interface IAppEventConnector
    {
        Task<AppEvent> GetAppEvent(Guid id, AppEventPropertyHelper helper);
        Task Create(AppEvent newAppEvent);
        Task DeleteAppEvent(Guid id);
        Task Create(IEnumerable<DalAppEvent> dalAppEvents);
        Task<AppEvent> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class AppEventConnector : IAppEventConnector
    {
        private readonly IAppEventRepository _appEventRepository;
        private readonly IMediator _mediator;

        public AppEventConnector(
            IAppEventRepository appEventRepository,
            IMediator mediator
            )
        {
            _appEventRepository = appEventRepository;
            _mediator = mediator;
        }

        public async Task<AppEvent> GetAppEvent(Guid id, AppEventPropertyHelper helper)
        {
            return (await _mediator.Send(new AppEventByIdRequest(id, helper))).Result;
        }

        public async Task Create(AppEvent newAppEvent)
        {
            await _appEventRepository
                .Add(newAppEvent.CreateDALAppEvent());
        }

        public async Task DeleteAppEvent(Guid id)
        {
            await _appEventRepository.DeleteByObject(new DalAppEvent { Id = id });
        }

        public async Task Create(IEnumerable<DalAppEvent> dalAppEvents)
        {
            await _appEventRepository.Add(dalAppEvents);
        }

        public async Task<AppEvent> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var appEvent = await _appEventRepository
                .UpdateProperties(new DalAppEvent { Id = id }, properties);
            return appEvent.CreateAppEvent();
        }
    }
}
