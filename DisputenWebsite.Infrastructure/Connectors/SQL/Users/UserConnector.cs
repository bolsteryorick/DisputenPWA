using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests;
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
            return (await _mediator.Send(new UserByIdRequest(id, helper))).Result;
        }
    }
}
