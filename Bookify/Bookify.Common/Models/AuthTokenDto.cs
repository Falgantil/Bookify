using System.Collections;
using System.Collections.Generic;

namespace Bookify.Common.Models
{
    public class AuthTokenDto
    {
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}