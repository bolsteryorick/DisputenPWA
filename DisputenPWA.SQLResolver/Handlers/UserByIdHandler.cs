using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.SQLResolver.Members.MembersByUserIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Handlers
{
    public class UserByIdHandler : IRequestHandler<UserByIdRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public UserByIdHandler(
            IUserRepository userRepository,
            IMediator mediator
            )
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<User> Handle(UserByIdRequest request, CancellationToken cancellationToken)
        {
            return await ResolveUserById(request.UserId, request.Helper, cancellationToken);
        }

        private async Task<User> ResolveUserById(string userId, UserPropertyHelper helper, CancellationToken cancellationToken)
        {
            var user = await GetUserById(userId, helper);
            if (helper.CanGetMembers())
            {
                user.Memberships = await _mediator.Send(new MembersByUserIdsRequest(new List<string> { userId }, helper.MembershipsPropertyHelper), cancellationToken);
            }
            return user;
        }



        private async Task<User> GetUserById(string id, UserPropertyHelper helper)
        {
            var queryable = _userRepository.GetQueryable().Where(x => x.Id == id);
            return await _userRepository.GetFirstOrDefault(queryable, helper);
        }
    }
}