using System;
using System.Collections.Generic;
using System.Text;
using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.GoogleAccessInfoAggregate;

namespace DisputenPWA.DAL.Repositories
{
    public interface IGoogleAccessInfoRepository : IRepository<GoogleAccessInfo>
    {

    }

    public class GoogleAccessInfoRepository : Repository<GoogleAccessInfo>, IGoogleAccessInfoRepository
    {
        public GoogleAccessInfoRepository(DisputenAppContext context) : base(context)
        {
        }
    }
}