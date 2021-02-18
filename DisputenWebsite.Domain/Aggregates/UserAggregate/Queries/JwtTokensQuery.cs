using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries
{
    public class JwtTokensQuery : IRequest<JwtTokensQueryResult>
    {
        public JwtTokensQuery(string email, string password, string appInstanceId)
        {
            Email = email;
            Password = password;
            AppInstanceId = appInstanceId;
        }

        public string Email { get; }
        public string Password { get; }
        public string AppInstanceId { get; }
    }
}
