using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManager.Models;

namespace StockManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<StockPortfolio> StockPortfolios { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ShareBlock> ShareBlocks { get; set; }
        public DbSet<StockPortfolioStockMapping> SpsMappings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<StockPortfolioStockMapping>().HasKey(x => new { x.StockPortfolioId, x.StockId });
        }
    }
}
