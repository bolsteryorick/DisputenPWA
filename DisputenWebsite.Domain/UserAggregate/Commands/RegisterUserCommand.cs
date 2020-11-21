using DisputenPWA.Domain.UserAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.UserAggregate.Commands
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
