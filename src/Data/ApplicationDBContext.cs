using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MarketTrustAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {}

        public DbSet<TrustRating> TrustRatings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<TrustRating>()
                .HasOne(tr => tr.Trustor)
                .WithMany(u => u.TrustRatingsAsTrustor)
                .HasForeignKey(tr => tr.TrustorId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<TrustRating>()
                .HasOne(tr => tr.Trustee)
                .WithMany(u => u.TrustRatingsAsTrustee)
                .HasForeignKey(tr => tr.TrusteeId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}