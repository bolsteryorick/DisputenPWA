using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate.Commands.Results
{
    public class RegisterUserCommandResult : CommandResult<User>
    {
        public RegisterUserCommandResult(User user) : base(user)
        {

        }
    }
}