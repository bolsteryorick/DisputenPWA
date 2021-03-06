using DisputenPWA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Services
{
    public interface IBusinessAuthorizer
    {
        Task<bool> CanAddAttendee(Guid appEventId);
    }

    public class BusinessAuthorizer : IBusinessAuthorizer
    {
        private readonly IAppEventRepository _appEventRepository;

        public BusinessAuthorizer(
            IAppEventRepository appEventRepository
            )
        {
            _appEventRepository = appEventRepository;
        }

        public async Task<bool> CanAddAttendee(Guid appEventId)
        {
            return await _appEventRepository.GetQueryable().AnyAsync(x => x.Id == appEventId && (x.MaxAttendees == null || x.MaxAttendees > x.Attendances.Count));
        }
    }
}
