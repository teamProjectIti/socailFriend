using Data.Models;
using Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<test> tests { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserLike> Likes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLike>()
                .HasKey(x => new { x.SourceUserID, x.LikedUserID });

            modelBuilder.Entity<UserLike>()
                .HasOne(x => x.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserID)
                .OnDelete(DeleteBehavior.Cascade); 
            
            
            modelBuilder.Entity<UserLike>()
                .HasOne(x => x.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
