using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Users
{
    public interface IUserConnector
    {
        Task<User> GetUser(string id, UserPropertyHelper helper);
    }

    public class UserConnector : IUserConnector
    {
        private readonly IUserRepository _userRepository;
        private readonly IGraphQLResolver _graphQLResolver;

        public UserConnector(
            IUserRepository userRepository,
            IGraphQLResolver graphQLResolver
            )
        {
            _userRepository = userRepository;
            _graphQLResolver = graphQLResolver;
        }

        public async Task<User> GetUser(string id, UserPropertyHelper helper)
        {
            return await _graphQLResolver.ResolveUserById(id, helper);
        }
    }
}
