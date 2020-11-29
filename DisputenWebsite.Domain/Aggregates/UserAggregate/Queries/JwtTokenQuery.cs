using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries
{
    public class JwtTokenQuery : IRequest<JwtTokenQueryResult>
    {
        public JwtTokenQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
