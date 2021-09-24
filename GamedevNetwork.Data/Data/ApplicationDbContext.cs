using Microsoft.EntityFrameworkCore;
using GamedevNetwork.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GamedevNetwork.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GamedevProfile> GamedevProfile { get; set; }
        public DbSet<Post> Post { get; set; }

    }
}