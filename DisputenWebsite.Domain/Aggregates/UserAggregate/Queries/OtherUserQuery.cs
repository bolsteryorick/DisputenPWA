using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries
{
    public class OtherUserQuery : IRequest<OtherUserQueryResult>
    {
        public OtherUserQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
