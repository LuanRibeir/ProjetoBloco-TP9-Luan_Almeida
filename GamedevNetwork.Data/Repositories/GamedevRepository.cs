using GamedevNetwork.Data.Data;
using GamedevNetwork.Domain.Interfaces;
using GamedevNetwork.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamedevNetwork.Data.Repositories
{
    public class GamedevRepository : IGamedevRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public GamedevRepository(ApplicationDbContext context,
                                 UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<GamedevProfile>> GetAllAsync()
        {
            return await _context.GamedevProfile.ToListAsync();
        }

        public async Task<IEnumerable<GamedevProfile>> GetProfileAsync(string userId)
        {
            return await _context.GamedevProfile.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<GamedevProfile> GetByIdAsync(int id)
        {
            return await _context.GamedevProfile.AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(GamedevProfile gamedevProfile)
        {
            _context.Add(gamedevProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GamedevProfile gamedevProfile)
        {
            _context.Update(gamedevProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeletetAsync(GamedevProfile gamedevProfile)
        {
            _context.GamedevProfile.Remove(gamedevProfile);
            await _context.SaveChangesAsync();
        }
    }
}