using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class OtherUserQueryHandler : IRequestHandler<OtherUserQuery, OtherUserQueryResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IUserConnector _userConnector;

        public OtherUserQueryHandler(
            IOperationAuthorizer operationAuthorizer,
            IUserConnector userConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _userConnector = userConnector;
        }

        public async Task<OtherUserQueryResult> Handle(OtherUserQuery request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanSeeOtherUser(request.UserId))
            {
                var otherUser = await _userConnector.GetUserById(request.UserId);
                return new OtherUserQueryResult(otherUser);
            }
            return new OtherUserQueryResult(null);
        }
    }
}
