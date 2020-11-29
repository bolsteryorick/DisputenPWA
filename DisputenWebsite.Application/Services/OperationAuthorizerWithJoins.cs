using DisputenPWA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Services
{
    public class OperationAuthorizerWithJoins : IOperationAuthorizer
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAppEventRepository _appEventRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly string _userId;

        public OperationAuthorizerWithJoins(
            IMemberRepository memberRepository,
            IAppEventRepository appEventRepository,
            IUserService userService,
            IAttendeeRepository attendeeRepository
            )
        {
            _memberRepository = memberRepository;
            _appEventRepository = appEventRepository;
            _attendeeRepository = attendeeRepository;
            _userId = userService.GetUserId();
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
            return await _attendeeRepository.GetQueryable().AnyAsync(x => x.Id == attendeeId && x.AppEvent.Group.Members.Any(m => m.UserId == _userId && m.IsAdmin));
        }
        private async Task<bool> IsInGroupWithAttendee(Guid attendeeId)
        {
            return await _attendeeRepository.GetQueryable().AnyAsync(x => x.Id == attendeeId && x.AppEvent.Group.Members.Any(m => m.UserId == _userId));
        }
        private async Task<bool> IsThisMember(Guid memberId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.Id == memberId);
        }
        private async Task<bool> IsAdminOfGroupWithMember(Guid memberId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(m => m.Id == memberId && m.Group.Members.Any(m2 => m2.UserId == _userId && m.IsAdmin));
        }
        private async Task<bool> IsInGroupWithMember(Guid memberId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(m => m.Id == memberId && m.Group.Members.Any(m2 => m2.UserId == _userId));
        }
        private async Task<bool> IsInAppEvent(Guid appEventId)
        {
            return await _appEventRepository.GetQueryable().AnyAsync(a => a.Id == appEventId && a.Group.Members.Any(m => m.UserId == _userId));
        }
        private async Task<bool> IsAppEventAdmin(Guid appEventId)
        {
            return await _appEventRepository.GetQueryable().AnyAsync(x => x.Id == appEventId && x.Group.Members.Any(y => y.UserId == _userId && y.IsAdmin));
        }
        private async Task<bool> IsInGroup(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId);
        }
        private async Task<bool> IsGroupAdmin(Guid groupId)
        {
            return await _memberRepository.GetQueryable().AnyAsync(x => x.UserId == _userId && x.GroupId == groupId && x.IsAdmin);
        }
    }
}
