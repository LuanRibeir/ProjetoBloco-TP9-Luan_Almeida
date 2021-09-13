using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GamedevNetwork.Mvc.Models;

namespace GamedevNetwork.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GamedevNetwork.Mvc.Models.GamedevProfile> GamedevProfile { get; set; }
        public DbSet<GamedevNetwork.Mvc.Models.Post> Post { get; set; }

    }
}
