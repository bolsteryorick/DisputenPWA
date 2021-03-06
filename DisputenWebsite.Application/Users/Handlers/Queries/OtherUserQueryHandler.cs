using DisputenPWA.Application.Services;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IContactRepository _contactRepository;
        private readonly IUserService _userService;

        public OtherUserQueryHandler(
            IOperationAuthorizer operationAuthorizer,
            IUserConnector userConnector,
            IContactRepository contactRepository,
            IUserService userService
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _userConnector = userConnector;
            _contactRepository = contactRepository;
            _userService = userService;
        }

        public async Task<OtherUserQueryResult> Handle(OtherUserQuery request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanSeeOtherUser(request.UserId))
            {
                var otherUser = await _userConnector.GetUserById(request.UserId);
                otherUser.IsContact = await _contactRepository.GetQueryable().AnyAsync(c => c.UserId == _userService.GetUserId() && c.ContactUserId == request.UserId);
                return new OtherUserQueryResult(otherUser);
            }
            return new OtherUserQueryResult(null);
        }
    }
}
