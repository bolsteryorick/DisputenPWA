using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Services
{
    public interface IUserService
    {
        bool IsAuthorised();
        Task<ApplicationUser> GetUser();
        string GetUserId();
    }
    
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthorizedService _userAuthorizedService;
        private readonly IUserRepository _userRepository;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IUserAuthorizedService userAuthorizedService,
            IUserRepository userRepository
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _userAuthorizedService = userAuthorizedService;
            _userRepository = userRepository;
        }

        public bool IsAuthorised()
        {
            return _userAuthorizedService.IsAuthorised();
        }

        public async Task<ApplicationUser> GetUser()
        {
            var user = (ApplicationUser)_httpContextAccessor.HttpContext.Items["User"];
            if(user == null)
            {
                user = await _userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Id == GetUserId());
                _httpContextAccessor.HttpContext.Items["User"] = user;
            }
            return user;
        }

        public string GetUserId()
        {
            return (string)_httpContextAccessor.HttpContext.Items["UserId"];
        }
    }
}