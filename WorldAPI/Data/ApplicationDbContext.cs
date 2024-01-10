using Microsoft.EntityFrameworkCore;
using WorldAPI.Models;

namespace WorldAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
    }
}
