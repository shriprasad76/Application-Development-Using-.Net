using GarageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageAPI.Data
{
    /// <summary>
    /// EF Core DbContext for the garage vehicle management system.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
    }
}
