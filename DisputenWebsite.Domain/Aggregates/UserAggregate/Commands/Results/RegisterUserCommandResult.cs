using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results
{
    public class RegisterUserCommandResult : CommandResult<User>
    {
        public RegisterUserCommandResult(User user) : base(user)
        {

        }
    }
}