using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries
{
    public class UserQuery : IRequest<UserQueryResult>
    {
        public UserQuery(
            UserPropertyHelper userPropertyHelper
            )
        {
            UserPropertyHelper = userPropertyHelper;
        }

        public UserPropertyHelper UserPropertyHelper { get; }
    }
}