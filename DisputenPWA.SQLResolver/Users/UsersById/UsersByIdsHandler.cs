using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate;
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
        private readonly IResolveForUserService _resolveForUserService;

        public UsersByIdsHandler(
            IUserRepository userRepository,
            IResolveForUserService resolveForUserService
            )
        {
            _userRepository = userRepository;
            _resolveForUserService = resolveForUserService;
        }

        public async Task<IReadOnlyCollection<User>> Handle(UsersByIdsRequest req, CancellationToken cancellationToken)
        {
            var query = _userRepository.GetQueryable().Where(x => req.UserIds.Contains(x.Id));
            return await _resolveForUserService.Resolve(query, req.Helper, cancellationToken);
        }
    }
}
