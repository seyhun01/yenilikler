using Microsoft.EntityFrameworkCore;
using WareHause.Models;
namespace WareHause.Data
{
    public class AnbarDbContext:DbContext
    {
        public AnbarDbContext(DbContextOptions<AnbarDbContext> options):base(options)
        {
            
        }
        public DbSet<MIlkProduct>  MIlkProducts { get; set; }
        public DbSet<Chocolate> Chocolates { get; set; }
        public DbSet<Fruit> Fruits { get; set; }
    }
}
