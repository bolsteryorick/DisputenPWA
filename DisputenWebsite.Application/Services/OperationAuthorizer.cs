using DisputenPWA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Services
{
    public interface IOperationAuthorizer
    {
        Task<bool> CanSeeOtherUser(string otherUserId);
        Task<bool> CanQueryGroup(Guid groupId);
        Task<bool> CanUpdateGroup(Guid groupId);
        Task<bool> CanQueryAppEvent(Guid appEventId);
        Task<bool> CanChangeAppEvent(Guid appEventId);
        Task<bool> CanQueryMember(Guid memberId);
        Task<bool> CanChangeMember(Guid memberId);
        Task<bool> CanLeaveGroup(Guid memberId);
        Task<bool> CanJoinEvent(Guid appEventId);
        Task<bool> CanLeaveEvent(Guid attendeeId);
        Task<bool> CanQueryAttendee(Guid attendeeId);
        Task<bool> CanChangeAttendee(Guid attendeeId);
    }

    public class OperationAuthorizer : IOperationAuthorizer
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAppEventRepository _appEventRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IContactRepository _contactRepository;
        private readonly string _userId;

        public OperationAuthorizer(
            IMemberRepository memberRepository,
            IAppEventRepository appEventRepository,
            IUserService userService,
            IAttendeeRepository attendeeRepository,
            IContactRepository contactRepository
            )
        {
            _memberRepository = memberRepository;
            _appEventRepository = appEventRepository;
            _attendeeRepository = attendeeRepository;
            _contactRepository = contactRepository;
            _userId = userService.GetUserId();
        }

        public async Task<bool> CanSeeOtherUser(string otherUserId)
        {
            var isUserContact = await _contactRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.ContactUserId == otherUserId);
            if (isUserContact) return true;
            
            var combinedGroupIds = await _memberRepository.GetQueryable().Where(m => m.UserId == _userId || m.UserId == otherUserId).Select(x => x.GroupId).ToListAsync();
            var hasDouble = combinedGroupIds.Count > combinedGroupIds.Distinct().Count();
            return hasDouble;
        }

        public async Task<bool> CanQueryGroup(Guid groupId)
        {
            return await IsInGroup(groupId);
        }

        public async Task<bool> CanUpdateGroup(Guid groupId)
        {
            return await IsGroupAdmin(groupId);
        }

        public async Task<bool> CanQueryAppEvent(Guid appEventId)
        {
            return await IsInAppEvent(appEventId);
        }

        public async Task<bool> CanChangeAppEvent(Guid appEventId)
        {
            return await IsAppEventAdmin(appEventId);
        }

        public async Task<bool> CanQueryMember(Guid memberId)
        {
            return await IsInGroupWithMember(memberId);
        }

        public async Task<bool> CanChangeMember(Guid memberId)
        {
            return await IsAdminOfGroupWithMember(memberId);
        }

        public async Task<bool> CanLeaveGroup(Guid memberId)
        {
            return await IsThisMember(memberId);
        }

        public async Task<bool> CanJoinEvent(Guid appEventId)
        {
            return await IsInAppEvent(appEventId);
        }

        public async Task<bool> CanLeaveEvent(Guid attendeeId)
        {
            return await IsThisAttendee(attendeeId);
        }

        public async Task<bool> CanQueryAttendee(Guid attendeeId)
        {
            return await IsInGroupWithAttendee(attendeeId);
        }

        public async Task<bool> CanChangeAttendee(Guid attendeeId)
        {
            return await IsAdminOfGroupWithAttendee(attendeeId);
        }


        private async Task<bool> IsThisAttendee(Guid attendeeId)
        {
            return await _attendeeRepository.GetQueryable().AnyAsync(a => a.UserId == _userId && a.Id == attendeeId);
        }
        private async Task<bool> IsAdminOfGroupWithAttendee(Guid attendeeId)
        {
            var groupId = await GroupIdFromAttendeeId(attendeeId);
            return await IsGroupAdmin(groupId);
        }
        private async Task<bool> IsInGroupWithAttendee(Guid attendeeId)
        {
            var groupId = await GroupIdFromAttendeeId(attendeeId);
            return await IsInGroup(groupId);
        }
        private async Task<bool> IsThisMember(Guid memberId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.Id == memberId);
        }
        private async Task<bool> IsAdminOfGroupWithMember(Guid memberId)
        {
            var groupId = await GroupIdFromMemberId(memberId);
            return await CanUpdateGroup(groupId);
        }
        private async Task<bool> IsInGroupWithMember(Guid memberId)
        {
            var groupId = await GroupIdFromMemberId(memberId);
            return await IsInGroup(groupId);
        }
        private async Task<bool> IsInAppEvent(Guid appEventId)
        {
            var groupId = await GroupIdFromEventId(appEventId);
            return await IsInGroup(groupId);
        }
        private async Task<bool> IsAppEventAdmin(Guid appEventId)
        {
            var groupId = await GroupIdFromEventId(appEventId);
            return await IsGroupAdmin(groupId);
        }
        private async Task<bool> IsInGroup(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId);
        }
        private async Task<bool> IsGroupAdmin(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId && x.IsAdmin);
        }
        private async Task<Guid> GroupIdFromEventId(Guid appEventId)
        {
            return await _appEventRepository.GetQueryable().Where(x => x.Id == appEventId).Select(x => x.GroupId).FirstOrDefaultAsync();
        }
        private async Task<Guid> GroupIdFromMemberId(Guid memberId)
        {
            return await _memberRepository.GetQueryable().Where(x => x.Id == memberId).Select(x => x.GroupId).FirstOrDefaultAsync();
        }
        private async Task<Guid> GroupIdFromAttendeeId(Guid attendeeId)
        {
            return await _attendeeRepository.GetQueryable().Where(x => x.Id == attendeeId).Select(x => x.AppEvent.GroupId).FirstOrDefaultAsync();
        }
    }
}
