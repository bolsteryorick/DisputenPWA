using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries
{
    public class RefreshJwtTokensQuery : IRequest<JwtTokensQueryResult>
    {
        public RefreshJwtTokensQuery(string refreshToken, string appInstanceId)
        {
            RefreshToken = refreshToken;
            AppInstanceId = appInstanceId;
        }

        public string RefreshToken { get; }
        public string AppInstanceId { get; }
    }
}
