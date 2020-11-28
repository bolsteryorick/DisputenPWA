using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories.Base;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.DalObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories
{
    public interface IMemberRepository : IRepository<DalMember>
    {
        Task<Member> GetFirstOrDefault(IQueryable<DalMember> queryable, MemberPropertyHelper helper);
        Task<List<Member>> GetAll(IQueryable<DalMember> queryable, MemberPropertyHelper helper);
    }

    public class MemberRepository : Repository<DalMember>, IMemberRepository
    {
        public MemberRepository(DisputenAppContext context) : base(context)
        {

        }

        public async Task<Member> GetFirstOrDefault(IQueryable<DalMember> queryable, MemberPropertyHelper helper)
        {
            var selectMemberQuery = SelectMember(queryable, helper);
            return await selectMemberQuery.FirstOrDefaultAsync();
        }

        public async Task<List<Member>> GetAll(IQueryable<DalMember> queryable, MemberPropertyHelper helper)
        {
            var selectMemberQuery = SelectMember(queryable, helper);
            return await selectMemberQuery.ToListAsync();
        }

        private IQueryable<Member> SelectMember(IQueryable<DalMember> queryable, MemberPropertyHelper helper)
        {
            return queryable
                .Select(x =>
                    new Member
                    {
                        Id = x.Id,
                        GroupId = x.GroupId,
                        UserId = x.UserId,
                        IsAdmin = helper.GetIsAdmin ? x.IsAdmin : false,
                    });
        }
    }
}