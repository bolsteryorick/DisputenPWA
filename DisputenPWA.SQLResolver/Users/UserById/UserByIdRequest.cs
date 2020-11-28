using DisputenPWA.Domain.Aggregates.UserAggregate;
using MediatR;

namespace DisputenPWA.SQLResolver.Users.UserById
{
    public class UserByIdRequest : IRequest<User>
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