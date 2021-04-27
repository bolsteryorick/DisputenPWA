using System;
using System.Collections.Generic;
using System.Text;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Commands
{
    public class LoginWithGoogleCommand : IRequest<RegisterUserCommandResult>
    {
        public LoginWithGoogleCommand(string token, string appInstanceId)
        {
            Token = token;
            AppInstanceId = appInstanceId;
        }

        public string Token { get; }
        public string AppInstanceId { get; }
    }
}
