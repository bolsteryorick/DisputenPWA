using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.Users.UserById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Users
{
    public interface IUserConnector
    {
        Task<User> GetUser(string id, UserPropertyHelper helper);
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetUsersByEmail(IEnumerable<string> emailAddresses);
    }

    public class UserConnector : IUserConnector
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UserConnector(
            IMediator mediator,
            IUserRepository userRepository
            )
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<User> GetUser(string id, UserPropertyHelper helper)
        {
            return await _mediator.Send(new UserByIdRequest(id, helper));
        }

        public async Task<User> GetUserById(string id)
        {
            return (await _userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Id == id)).CreateUser();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var users = await GetUsersByEmail(new List<string> { email });
            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetUsersByEmail(IEnumerable<string> emails)
        {
            var emailAddressesUpper = emails.Select(e => e.ToUpper());
            var users = await _userRepository.GetQueryable().Where(c => emailAddressesUpper.Contains(c.NormalizedEmail)).ToListAsync();
            return users.Select(x => x.CreateUser());
        }
    }
}
