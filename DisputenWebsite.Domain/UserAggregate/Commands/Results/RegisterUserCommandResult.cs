using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.UserAggregate.Commands.Results
{
    public class RegisterUserCommandResult : CommandResult<User>
    {
        public RegisterUserCommandResult(User user) : base(user)
        {

        }
    }
}