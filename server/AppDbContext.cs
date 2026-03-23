using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // 這行代表 MySQL 裡會有一張叫 Products 的資料表
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Product>().HasData(
        new Product { Id = 1, Name = "經典大漢堡", Price = 120, Description = "厚切牛肉配上特製醬汁", Category = "主食" },
        new Product { Id = 2, Name = "脆皮薯條", Price = 50, Description = "現炸金黃酥脆", Category = "小吃" },
        new Product { Id = 3, Name = "冰鎮可樂", Price = 35, Description = "清涼解渴", Category = "飲料" }
    );
  }
}

