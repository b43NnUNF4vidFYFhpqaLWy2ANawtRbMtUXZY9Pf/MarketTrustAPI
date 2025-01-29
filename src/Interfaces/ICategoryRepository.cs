using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> ExistAsync(int id);
    }
}