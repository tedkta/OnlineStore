using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Data;

public class ApplicationDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ProductsModel> Products { get; set; }
    public DbSet<PayOut> PayOuts { get; set; }
    public DbSet<ImageModel> Images { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;Database=OnlineStore_db;User=root;Password=1234;Port=3306");
    }
}