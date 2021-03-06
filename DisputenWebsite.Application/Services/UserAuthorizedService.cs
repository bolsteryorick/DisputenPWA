using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Application.Services
{
    public interface IUserAuthorizedService
    {
        bool IsAuthorised();
    }

    public class UserAuthorizedService : IUserAuthorizedService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAuthorizedService(
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthorised()
        {
            var authorized = _httpContextAccessor.HttpContext.Items["Authorized"];
            if (authorized == null) return false;
            return (bool)authorized;
        }
    }
}
