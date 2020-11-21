using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Handlers
{
    public class UserByIdHandler : IRequestHandler<UserByIdRequest, UserByIdResult>
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

        public async Task<UserByIdResult> Handle(UserByIdRequest request, CancellationToken cancellationToken)
        {
            return new UserByIdResult(
                await ResolveUserById(request.UserId, request.Helper, cancellationToken)
                );
        }

        private async Task<User> ResolveUserById(string userId, UserPropertyHelper helper, CancellationToken cancellationToken)
        {
            var user = await GetUserById(userId, helper);
            if (helper.CanGetMembers())
            {
                user.Memberships = (await _mediator.Send(new MembersByUserIdsRequest(new List<string> { userId }, helper.MembershipsPropertyHelper), cancellationToken)).Result;
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