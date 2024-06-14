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
        public DbSet<Categories> Categories { get; set; }
        public DbSet<AccountHistory> AccountHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.email).IsRequired();
                entity.Property(e => e.password).IsRequired();
                entity.Property(e => e.createdAt).IsRequired();
                entity.Property(e => e.updatedAt).IsRequired(); ;
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired();
                entity.Property(e => e.balance).IsRequired().HasColumnType("decimal(18, 2)");
                entity.Property(e => e.createdAt).IsRequired();
                entity.Property(e => e.updatedAt).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.type).IsRequired();
                entity.Property(e => e.amount).HasColumnType("decimal(15, 2)").IsRequired();
                entity.Property(e => e.createdAt).IsRequired();
                entity.Property(e => e.description);
                entity.Property(e => e.note);
                entity.Property(e => e.createdAt).IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Transactions)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Account)
                    .WithMany(a => a.Transactions)
                    .HasForeignKey(e => e.accountId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Category)
                    .WithMany(a => a.Transaction)
                    .HasForeignKey(e => e.categoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<AccountHistory>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.amount).HasColumnType("decimal(15, 2)").IsRequired();
                entity.Property(e => e.balance).HasColumnType("decimal(15, 2)").IsRequired();
                entity.Property(e => e.createdAt).IsRequired();

                entity.HasOne(e => e.Users)
                    .WithMany(u => u.AccountHistory)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Account)
                    .WithMany(a => a.AccountHistory)
                    .HasForeignKey(e => e.accountId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Transaction)
                    .WithMany(a => a.AccountHistory)
                    .HasForeignKey(e => e.transactionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Category)
                    .WithMany(a => a.AccountHistory)
                    .HasForeignKey(e => e.categoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
