using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.Users.UserById;
using MediatR;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Users
{
    public interface IUserConnector
    {
        Task<User> GetUser(string id, UserPropertyHelper helper);
    }

    public class UserConnector : IUserConnector
    {
        private readonly IMediator _mediator;

        public UserConnector(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<User> GetUser(string id, UserPropertyHelper helper)
        {
            return await _mediator.Send(new UserByIdRequest(id, helper));
        }
    }
}
