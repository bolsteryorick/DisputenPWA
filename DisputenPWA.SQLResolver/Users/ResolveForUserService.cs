using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.SQLResolver.Attendees.AttendeesByUserIds;
using DisputenPWA.SQLResolver.Helpers;
using DisputenPWA.SQLResolver.Members.MembersByUserIds;
using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Users
{
    public interface IResolveForUserService
    {
        Task<IReadOnlyCollection<User>> Resolve(
            IQueryable<ApplicationUser> query,
            UserPropertyHelper helper,
            CancellationToken cancellationToken);
    }

    public class ResolveForUserService : IResolveForUserService
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public ResolveForUserService(
            IMediator mediator,
            IUserRepository userRepository
            )
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyCollection<User>> Resolve(
            IQueryable<ApplicationUser> query, 
            UserPropertyHelper helper, 
            CancellationToken cancellationToken
            )
        {
            var users = await _userRepository.GetAll(query, helper);
            users = await AddForeignObjects(users, helper, cancellationToken);
            return users.ToImmutableList();
        }

        private async Task<IList<User>> AddForeignObjects(
            IList<User> users,
            UserPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            if (helper.CanGetMembers())
            {
                var members = await GetMembers(users, helper, cancellationToken);
                users = AddMembersToUsers(members, users);
            }
            if (helper.CanGetAttendences())
            {
                var attendees = await GetAttendees(users, helper, cancellationToken);
                users = AddAttendeesToUsers(attendees, users);
            }
            return users;
        }

        private async Task<IReadOnlyCollection<Member>> GetMembers(
            IList<User> users,
            UserPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var userIds = users.Select(x => x.Id);
            return await _mediator.Send(new MembersByUserIdsRequest(userIds, helper.MembershipsPropertyHelper), cancellationToken);
        }

        private async Task<IReadOnlyCollection<Attendee>> GetAttendees(
            IList<User> users,
            UserPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var userIds = users.Select(x => x.Id);
            return await _mediator.Send(new AttendeesByUserIdsRequest(userIds, helper.AttendeePropertyHelper), cancellationToken);
        }

        private IList<User> AddMembersToUsers(
            IReadOnlyCollection<Member> members,
            IList<User> users
            )
        {
            var userIdToMembershipDict = DictionaryMaker.MakeDictionary<string, Member>(nameof(Attendee.UserId), members);
            foreach (var user in users)
            {
                if (userIdToMembershipDict.TryGetValue(user.Id, out var userMemberships)) user.Memberships = userMemberships;
            }
            return users;
        }

        private IList<User> AddAttendeesToUsers(
            IReadOnlyCollection<Attendee> attendees,
            IList<User> users
            )
        {
            var userIdToAttendencesDict = DictionaryMaker.MakeDictionary<string, Attendee>(nameof(Attendee.UserId), attendees);
            foreach (var user in users)
            {
                if (userIdToAttendencesDict.TryGetValue(user.Id, out var userAttendences)) user.Attendences = userAttendences;
            }
            return users;
        }
    }
}