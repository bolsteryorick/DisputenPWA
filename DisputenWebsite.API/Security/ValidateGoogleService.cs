using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;

namespace DisputenPWA.API.Security
{
    public interface IValidateGoogleService
    {
        Task<bool> ValidateToken(string token);
    }

    public class ValidateGoogleService : IValidateGoogleService
    {
        public async Task<bool> ValidateToken(string token)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
            return validPayload != null;
        }
    }
}
