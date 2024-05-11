using ClinicSoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftware.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}
