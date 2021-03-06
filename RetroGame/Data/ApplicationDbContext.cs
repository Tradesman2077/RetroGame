using Microsoft.EntityFrameworkCore;
using RetroGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Platform> Platform { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Game> Game { get; set; }

    }
}
