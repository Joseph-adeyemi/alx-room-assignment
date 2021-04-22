using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
