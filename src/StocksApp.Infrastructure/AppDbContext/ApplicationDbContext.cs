using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StocksApp.Core.Domain.Entities;
using StocksApp.Core.Domain.IdentityEntities;

namespace StocksApp.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    { 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        } 

        public virtual DbSet<BuyOrder> BuyOrders { get; set; }
        public virtual DbSet<SellOrder> SellOrders { get; set; }
        public virtual DbSet<UserAccountBalance> UserAccountBalances { get; set; }
        public virtual DbSet<UserStock> UserStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Bind the DbSet properties to a self-named table
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
            modelBuilder.Entity<UserAccountBalance>().ToTable("UserAccountBalances");
            modelBuilder.Entity<UserStock>().HasKey(m => new { m.UserID, m.StockSymbol, m.StockName });
            modelBuilder.Entity<UserStock>().ToTable("UserStocks");
        }
    }
}