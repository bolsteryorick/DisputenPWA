using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.DAL.Repositories
{
    public interface IOutsideContactRepository : IRepository<DalOutsideContact>
    {

    }

    public class OutsideContactRepository : Repository<DalOutsideContact>, IOutsideContactRepository
    {
        public OutsideContactRepository(DisputenAppContext context) : base(context)
        {
        }
    }
}
