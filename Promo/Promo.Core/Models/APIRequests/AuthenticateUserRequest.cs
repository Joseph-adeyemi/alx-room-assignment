using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Models.APIRequests
{
    public class AuthenticateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
