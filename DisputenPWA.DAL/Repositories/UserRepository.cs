using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<User> GetFirstOrDefault(IQueryable<ApplicationUser> queryable, UserPropertyHelper helper);
        Task<List<User>> GetAll(IQueryable<ApplicationUser> queryable, UserPropertyHelper helper);
        Task<int> GetCount();
    }

    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<User> GetFirstOrDefault(IQueryable<ApplicationUser> queryable, UserPropertyHelper helper)
        {
            var selectMemberQuery = SelectUser(queryable, helper);
            return await selectMemberQuery.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll(IQueryable<ApplicationUser> queryable, UserPropertyHelper helper)
        {
            var selectMemberQuery = SelectUser(queryable, helper);
            return await selectMemberQuery.ToListAsync();
        }

        public async Task<int> GetCount()
        {
            return await GetQueryable().CountAsync();
        }

        private IQueryable<User> SelectUser(IQueryable<ApplicationUser> queryable, UserPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new User
                    {
                        Id = x.Id,
                        Email = helper.GetEmail ?  x.Email : null,
                        UserName = helper.GetUserName ? x.UserName : null,
                    });
        }
    }
}