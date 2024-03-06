using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp
{
    public class PostgreDbContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

        //Constructor je potreban za injectanje ove instance da bi imali jednu perzistentnu konekciju na bazu.
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
        : base(options)
        {
        }

    }
}
