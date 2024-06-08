using Microsoft.EntityFrameworkCore;
using OganiShoppingProject.Models.MaintoMain;
using OganiShoppingProject.Models;

namespace OganiShoppingProject.Data
{
    public class ShoppingDbContext:DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options):base(options)
        {
             
        }
        public DbSet<Settings> Settingss { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DbSet<News> Newss { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleoffProduct> SaleoffProducts { get; set; }
        public DbSet<Sizes> Sizess { get; set; }
        public DbSet<Slide> Slides { get; set; }

        public DbSet<SizeofFavoriteProduct> SizeofFavoriteProducts { get; set; }
        public DbSet<SizeofProduct> SizeofProducts { get; set; }
        public DbSet<SizeofSaleoffProduct> SizeofSaleoffProducts { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}
