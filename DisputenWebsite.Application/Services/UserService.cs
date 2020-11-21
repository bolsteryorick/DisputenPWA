using DisputenPWA.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;

namespace DisputenPWA.Application.Services
{
    public interface IUserService
    {
        bool IsAuthorised();
        ApplicationUser GetUser();
        string GetUserId();
    }
    
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IHttpContextAccessor httpContextAccessor 
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthorised()
        {
            return GetUser() != null;
        }

        public ApplicationUser GetUser()
        {
            return (ApplicationUser)_httpContextAccessor.HttpContext.Items["User"];
        }

        public string GetUserId()
        {
            return ((ApplicationUser)_httpContextAccessor.HttpContext.Items["User"]).Id;
        }
    }
}