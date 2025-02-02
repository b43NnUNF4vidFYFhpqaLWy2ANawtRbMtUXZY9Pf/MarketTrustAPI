using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}