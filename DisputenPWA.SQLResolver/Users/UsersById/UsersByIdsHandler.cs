using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.Attendees.AttendeesByUserIds;
using DisputenPWA.SQLResolver.Members.MembersByUserIds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Users.UsersById
{
    public class UsersByIdsHandler : IRequestHandler<UsersByIdsRequest, IReadOnlyCollection<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public UsersByIdsHandler(
            IUserRepository userRepository,
            IMediator mediator
            )
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<IReadOnlyCollection<User>> Handle(UsersByIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveUsersByIds(request.UserIds, request.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<User>> ResolveUsersByIds(IEnumerable<string> userIds, UserPropertyHelper helper, CancellationToken cancellationToken)
        {
            var users = await GetUsersByIds(userIds, helper);
            if (helper.CanGetMembers())
            {
                users = await ResolveMembersForUsers(users, userIds, helper, cancellationToken);
            }
            if (helper.CanGetAttendences())
            {
                users = await ResolveAttendencesForUsers(users, userIds, helper, cancellationToken);
            }
            return users.ToList();
        }

        private async Task<IEnumerable<User>> GetUsersByIds(IEnumerable<string> userIds, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => userIds.Contains(x.Id));
            return await _userRepository.GetAll(queryable, helper);
        }

        private async Task<IEnumerable<User>> ResolveMembersForUsers(IEnumerable<User> users, IEnumerable<string> userIds, UserPropertyHelper helper, CancellationToken cancellationToken)
        {
            var memberships = await _mediator.Send(new MembersByUserIdsRequest(userIds, helper.MembershipsPropertyHelper), cancellationToken);
            var userIdToMembershipDict = GetUserIdToMembershipsDict(memberships);
            foreach (var user in users)
            {
                if (userIdToMembershipDict.TryGetValue(user.Id, out var userMemberships)) user.Memberships = userMemberships;
            }
            return users;
        }

        private Dictionary<string, List<Member>> GetUserIdToMembershipsDict(IReadOnlyCollection<Member> items)
        {
            var dict = new Dictionary<string, List<Member>>();
            foreach (var item in items)
            {
                var userId = item.UserId;
                if (!dict.ContainsKey(userId))
                {
                    dict[userId] = new List<Member>();
                }
                dict[userId].Add(item);
            }
            return dict;
        }

        private async Task<IEnumerable<User>> ResolveAttendencesForUsers(IEnumerable<User> users, IEnumerable<string> userIds, UserPropertyHelper helper, CancellationToken cancellationToken)
        {
            var attendences = await _mediator.Send(new AttendeesByUserIdsRequest(userIds, helper.AttendeePropertyHelper), cancellationToken);
            var userIdToAttendencesDict = GetUserIdToAttendeesDict(attendences);
            foreach (var user in users)
            {
                if (userIdToAttendencesDict.TryGetValue(user.Id, out var userAttendences)) user.Attendences = userAttendences;
            }
            return users;
        }

        private Dictionary<string, List<Attendee>> GetUserIdToAttendeesDict(IReadOnlyCollection<Attendee> items)
        {
            var dict = new Dictionary<string, List<Attendee>>();
            foreach (var item in items)
            {
                var userId = item.UserId;
                if (!dict.ContainsKey(userId))
                {
                    dict[userId] = new List<Attendee>();
                }
                dict[userId].Add(item);
            }
            return dict;
        }
    }
}
