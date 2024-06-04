using FinanceApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.createdAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.accountName).IsRequired();
                entity.Property(e => e.balance).HasColumnType("decimal(15, 2)").HasDefaultValue(0.00);
                entity.Property(e => e.createdAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
                entity.Property(e => e.updatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.transactionType).IsRequired();
                entity.Property(e => e.amount).HasColumnType("decimal(15, 2)").IsRequired();
                entity.Property(e => e.transactionDate).IsRequired();
                entity.Property(e => e.description);
                entity.Property(e => e.createdAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                entity.HasOne(e => e.Account)
                    .WithMany(a => a.Transactions)
                    .HasForeignKey(e => e.accountId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
