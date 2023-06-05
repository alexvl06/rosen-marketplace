using Microsoft.EntityFrameworkCore;
using Marketplace.Core.Model;
using System;
using System.IO;

namespace Marketplace.Dal
{
    public class MarketplaceContext: DbContext
    {
        public virtual DbSet<Offer> offers {get; set;}
        public virtual DbSet<Category> categories {get; set;}
        public virtual DbSet<User> users {get; set;}

        public MarketplaceContext(){}
        public MarketplaceContext(DbContextOptions<MarketplaceContext> options):base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            optionsBuilder.UseSqlite( $@"Data Source={path}\Marketplace.Dal\marketplace.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>(
                offer=>{
                    offer.ToTable("Offer");
                    offer.HasKey(o=>o.Id);
                    offer.HasOne(o=>o.Category).WithMany(c=>c.Offers).HasForeignKey(o=>o.CategoryId);
                    offer.HasOne(o=>o.User).WithMany(u=>u.Offers).HasForeignKey(o=>o.UserId);
                    offer.Property(o=>o.Description).IsRequired().HasMaxLength(200);
                    offer.Property(o=>o.Location).IsRequired().HasMaxLength(100);
                    offer.Property(o=>o.PictureUrl).IsRequired().HasMaxLength(150);
                    offer.Property(o=>o.PublishedOn).IsRequired().HasDefaultValue(DateTime.Now);
                    offer.Property(o=>o.Title).IsRequired().HasMaxLength(50);
                }
            );

            modelBuilder.Entity<User>(
                user=>{
                    user.ToTable("User");
                    user.HasKey(u=>u.Id);
                    user.Property(u=>u.Username).IsRequired().HasMaxLength(40);
                    user.Ignore(u=>u.Offers);
                }
            );

            modelBuilder.Entity<Category>(
                category=>{
                    category.ToTable("Category");
                    category.HasKey(c=>c.Id);
                    category.Property(c=>c.Name).IsRequired().HasMaxLength(20);
                    category.Ignore(c=>c.Offers);
                }
            );
        }
    }
}