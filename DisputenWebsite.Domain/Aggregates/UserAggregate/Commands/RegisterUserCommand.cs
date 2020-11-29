using DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Commands
{
    public class RegisterUserCommand : IRequest<RegisterUserCommandResult>
    {
        public RegisterUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
