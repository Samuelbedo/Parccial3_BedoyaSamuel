using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial3_BedoyaSamuel.DAL.Entities;

namespace Parcial3_BedoyaSamuel.DAL
{
    public class DataBaseContext : IdentityDbContext<User>    
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDetails> VehiclesDetails { get; set; }

    }
}
