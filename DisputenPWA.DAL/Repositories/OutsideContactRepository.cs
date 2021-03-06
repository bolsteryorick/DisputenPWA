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
    public interface IOutsideContactRepository : IRepository<DalOutsideContact>
    {
        Task<Contact> GetFirstOrDefault(IQueryable<DalOutsideContact> queryable);
        Task<IList<Contact>> GetAll(IQueryable<DalOutsideContact> queryable);
    }

    public class OutsideContactRepository : Repository<DalOutsideContact>, IOutsideContactRepository
    {
        public OutsideContactRepository(DisputenAppContext context) : base(context)
        {
        }

        public async Task<Contact> GetFirstOrDefault(IQueryable<DalOutsideContact> queryable)
        {
            var selectMemberQuery = queryable.Select(x => x.CreateContact());
            return await selectMemberQuery.FirstOrDefaultAsync();
        }

        public async Task<IList<Contact>> GetAll(IQueryable<DalOutsideContact> queryable)
        {
            var selectMemberQuery = queryable.Select(x => x.CreateContact());
            return await selectMemberQuery.ToListAsync();
        }
    }
}
