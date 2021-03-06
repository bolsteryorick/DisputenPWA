using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.DAL.Repositories
{
    public interface IRefreshTokenRepository: IRepository<DalRefreshToken>
    {

    }

    public class RefreshTokenRepository : Repository<DalRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DisputenAppContext context) : base(context)
        {
        }
    }
}
