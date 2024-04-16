using Bigbasket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Bigbasket.DataAccess.Data
{
    public class BigBasketDbContext : IdentityDbContext<IdentityUser>
    {
        public BigBasketDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        //public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fruits", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Vegetables", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Meat & SeaFood", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Milk", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Grocery", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Deals", DisplayOrder = 6 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Coloured Capsicum Mix",
                    Mrp = 80.00,
                    Price = 50.00,
                    Offer = 30.00, // Corrected to decimal value
                    ProductDescription = "Coloured Capsicum Mix",
                    AboutProduct = "Extremely juicy and syrupy, these conical heart shaped fruits have seeds on the skin that give them a unique texture.\r\n" +
                        "With a blend of sweet-tart flavour, these are everyone's favourite berries.",
                    Benefit = "Strawberries improve heart function.They are rich in antioxidants and detoxifiers, which reduce arthritis and gout pain.\r\n" +
                        "Folic acid present in strawberries is necessary for pregnant women as it helps prevent birth defects. They maintain bone, skin, hair and eye health.",
                    Uses = "Do not rinse berries under running water as the pressure can crush them." +
                        " Dip them in a bowl of cold water and allow the berries to drain.",
                    CategoryId = 2,
                    ImageUrl = ""
                }
                );
        }
    }
}
