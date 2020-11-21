﻿using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.SQLResolver.Members.MembersByUserIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Handlers
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
    }
}