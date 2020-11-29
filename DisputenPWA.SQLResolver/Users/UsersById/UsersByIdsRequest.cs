using DisputenPWA.Domain.Aggregates.UserAggregate;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Users.UsersById
{
    public class UsersByIdsRequest : IRequest<IReadOnlyCollection<User>>
    {
        public UsersByIdsRequest(
            IEnumerable<string> userIds,
            UserPropertyHelper helper
            )
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public UserPropertyHelper Helper { get; }
    }
}