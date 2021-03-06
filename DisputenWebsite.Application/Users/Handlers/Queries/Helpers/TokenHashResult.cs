using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Application.Users.Handlers.Queries.Helpers
{
    public class TokenHashResult
    {
        public string TokenHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
