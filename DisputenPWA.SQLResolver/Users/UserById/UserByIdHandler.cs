using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Users.UserById
{
    public class UserByIdHandler : IRequestHandler<UserByIdRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IResolveForUserService _resolveForUserService;

        public UserByIdHandler(
            IUserRepository userRepository,
            IResolveForUserService resolveForUserService
            )
        {
            _userRepository = userRepository;
            _resolveForUserService = resolveForUserService;
        }

        public async Task<User> Handle(UserByIdRequest req, CancellationToken cancellationToken)
        {
            var query = _userRepository.GetQueryable().Where(x => x.Id == req.UserId);
            var userInList = await _resolveForUserService.Resolve(query, req.Helper, cancellationToken);
            return userInList.FirstOrDefault();
        }
    }
}