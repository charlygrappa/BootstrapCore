using System;
using BootstrapCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BootstrapCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Inscription> Inscriptions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
    }
}
