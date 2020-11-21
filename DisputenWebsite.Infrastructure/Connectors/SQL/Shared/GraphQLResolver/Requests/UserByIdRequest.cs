using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class UserByIdRequest : IRequest<UserByIdResult>
    {
        public UserByIdRequest(
            string userId,
            UserPropertyHelper helper
            )
        {
            UserId = userId;
            Helper = helper;
        }

        public string UserId { get; }
        public UserPropertyHelper Helper { get; }
    }
}