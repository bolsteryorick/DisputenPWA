using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IContactRepository: IRepository<DalPlatformContact>
    {
        Task<Contact> GetFirstOrDefault(IQueryable<DalPlatformContact> queryable, ContactPropertyHelper helper);
        Task<IList<Contact>> GetAll(IQueryable<DalPlatformContact> queryable, ContactPropertyHelper helper);
    }

    public class ContactRepository : Repository<DalPlatformContact>, IContactRepository
    {
        public ContactRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<Contact> GetFirstOrDefault(IQueryable<DalPlatformContact> queryable, ContactPropertyHelper helper)
        {
            var selectMemberQuery = SelectMember(queryable, helper);
            return await selectMemberQuery.FirstOrDefaultAsync();
        }

        public async Task<IList<Contact>> GetAll(IQueryable<DalPlatformContact> queryable, ContactPropertyHelper helper)
        {
            var selectMemberQuery = SelectMember(queryable, helper);
            return await selectMemberQuery.ToListAsync();
        }

        private IQueryable<Contact> SelectMember(IQueryable<DalPlatformContact> queryable, ContactPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new Contact
                    {
                        Id = x.Id,
                        ContactUserId = x.ContactUserId,
                        UserId = x.UserId,
                    });
        }
    }
}
