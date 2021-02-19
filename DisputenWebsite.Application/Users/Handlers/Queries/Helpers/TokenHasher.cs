using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DisputenPWA.Application.Users.Handlers.Queries.Helpers
{
    public class TokenHasher
    {
        public static TokenHashResult HashToken(string token, byte[] passedSalt = null)
        {
            byte[] salt = GetSalt(passedSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashedToken = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: token,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new TokenHashResult
            {
                TokenHash = hashedToken,
                Salt = salt
            };
        }

        public static byte[] GetSalt(byte[] salt)
        {
            if (salt != null) return salt;
            byte[] newSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(newSalt);
            }
            return newSalt;
        }
    }
}
