using DisputenPWA.Domain.UserAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate.Queries
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
