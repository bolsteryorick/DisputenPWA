using DisputenPWA.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Members.Handlers
{
    public interface ILeaveAllGroupEventsService
    {
        Task LeaveAllGroupEvents(Guid memberId);
    }

    public class LeaveAllGroupEventsService : ILeaveAllGroupEventsService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public LeaveAllGroupEventsService(
            IMemberRepository memberRepository,
            IAttendeeRepository attendeeRepository
            )
        {
            _memberRepository = memberRepository;
            _attendeeRepository = attendeeRepository;
        }

        public async Task LeaveAllGroupEvents(Guid memberId)
        {
            var member = await _memberRepository.GetQueryable().FirstOrDefaultAsync(m => m.Id == memberId);
            var deleteQuery = _attendeeRepository.GetQueryable().Where(a => a.UserId == member.UserId && a.AppEvent.GroupId == member.GroupId);
            await _attendeeRepository.DeleteByQuery(deleteQuery);
        }
    }
}
