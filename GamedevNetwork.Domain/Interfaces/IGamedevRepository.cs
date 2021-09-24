using GamedevNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Domain.Interfaces
{
    public interface IGamedevRepository
    {
        Task<IEnumerable<GamedevProfile>> GetAllAsync();
        Task<GamedevProfile> GetByIdAsync(int id);
        Task InsertAsync(GamedevProfile gamedevProfile);
        Task UpdateAsync(GamedevProfile gamedevProfile);
        Task DeletetAsync(GamedevProfile gamedevProfile);
        Task<IEnumerable<GamedevProfile>> GetProfileAsync(string userId);
    }
}