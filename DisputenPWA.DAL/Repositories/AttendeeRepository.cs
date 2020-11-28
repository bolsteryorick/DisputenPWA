using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IAttendeeRepository : IRepository<DalAttendee>
    {
        Task<Attendee> GetFirstOrDefault(IQueryable<DalAttendee> queryable, AttendeePropertyHelper helper);
        Task<List<Attendee>> GetAll(IQueryable<DalAttendee> queryable, AttendeePropertyHelper helper);
    }

    public class AttendeeRepository : Repository<DalAttendee>, IAttendeeRepository
    {
        public AttendeeRepository(DisputenAppContext context) : base(context)
        {
        }

        public async Task<Attendee> GetFirstOrDefault(IQueryable<DalAttendee> queryable, AttendeePropertyHelper helper)
        {
            var query = SelectMember(queryable, helper);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Attendee>> GetAll(IQueryable<DalAttendee> queryable, AttendeePropertyHelper helper)
        {
            var query = SelectMember(queryable, helper);
            return await query.ToListAsync();
        }

        private IQueryable<Attendee> SelectMember(IQueryable<DalAttendee> queryable, AttendeePropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new Attendee
                    {
                        Id = x.Id,
                        AppEventId = x.AppEventId,
                        UserId = x.UserId,
                        Paid = helper.GetPaid ? x.Paid : false,
                    });
        }
    }
}
