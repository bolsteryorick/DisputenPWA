using DisputenPWA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Services
{
    public interface IOperationAuthorizer
    {
        Task<bool> CanQueryGroup(Guid groupId);
        Task<bool> CanUpdateGroup(Guid groupId);
        Task<bool> CanQueryAppEvent(Guid appEventId);
        Task<bool> CanChangeAppEvent(Guid appEventId);
        Task<bool> CanQueryMember(Guid memberId);
        Task<bool> CanChangeMember(Guid memberId);
        Task<bool> CanLeaveGroup(Guid memberId);
    }

    public class OperationAuthorizer : IOperationAuthorizer
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAppEventRepository _appEventRepository;
        private readonly string _userId;

        public OperationAuthorizer(
            IMemberRepository memberRepository,
            IAppEventRepository appEventRepository,
            IUserService userService
            )
        {
            _memberRepository = memberRepository;
            _appEventRepository = appEventRepository;
            _userId = userService.GetUserId();
        }

        public async Task<bool> CanQueryGroup(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId);
        }

        public async Task<bool> CanUpdateGroup(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId && x.IsAdmin);
        }

        public async Task<bool> CanQueryAppEvent(Guid appEventId)
        {
            var groupId = await GetGroupIdFromAppEventId(appEventId);
            return await CanQueryGroup(groupId);
        }

        public async Task<bool> CanChangeAppEvent(Guid appEventId)
        {
            var groupId = await GetGroupIdFromAppEventId(appEventId);
            return await CanUpdateGroup(groupId);
        }

        public async Task<bool> CanQueryMember(Guid memberId)
        {
            var groupId = await GetGroupIdFromMemberId(memberId);
            return await CanQueryGroup(groupId);
        }

        public async Task<bool> CanChangeMember(Guid memberId)
        {
            var groupId = await GetGroupIdFromMemberId(memberId);
            return await CanUpdateGroup(groupId);
        }

        public async Task<bool> CanLeaveGroup(Guid memberId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.Id == memberId);
        }

        private async Task<Guid> GetGroupIdFromAppEventId(Guid appEventId)
        {
            return await _appEventRepository.GetQueryable().Where(x => x.Id == appEventId).Select(x => x.GroupId).FirstOrDefaultAsync();
        }

        private async Task<Guid> GetGroupIdFromMemberId(Guid memberId)
        {
            return await _memberRepository.GetQueryable().Where(x => x.Id == memberId).Select(x => x.GroupId).FirstOrDefaultAsync();
        }


        //public async Task<bool> CanDoOperation(Operation opr)
        //{
        //    if(opr.Row == Row.Group)
        //    {
        //        return opr.Action switch
        //        {
        //            Action.query => await CanQueryGroup(opr.UserId, opr.ObjectId),
        //            Action.update => await CanUpdateGroup(opr.UserId, opr.ObjectId),
        //            Action.create => true,
        //            Action.delete => false,
        //            _ => false
        //        };
        //    }
        //    if(opr.Row == Row.AppEvent)
        //    {
        //        return opr.Action switch
        //        {
        //            Action.query => await CanQueryAppEvent(opr.UserId, opr.ObjectId),
        //            Action.update => await CanUpdateAppEvent(opr.UserId, opr.ObjectId),
        //            Action.create => await CanUpdateGroup(opr.UserId, opr.ObjectId),
        //            Action.delete => await CanUpdateGroup(opr.UserId, opr.ObjectId),
        //            _ => false
        //        };
        //    }
        //    if(opr.Row == Row.Member)
        //    {
        //        return opr.Action switch
        //        {
        //            Action.query => await CanQueryMember(opr.UserId, opr.ObjectId),
        //            Action.update => await CanUpdateMember(opr.UserId, opr.ObjectId),
        //            Action.create => await CanUpdateGroup(opr.UserId, opr.ObjectId),
        //            Action.delete => await CanUpdateGroup(opr.UserId, opr.ObjectId),
        //            _ => false
        //        };
        //    }
        //    return false;
        //}
    }
}
