using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Errors
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
