using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using barker.data;

namespace barker.repository.LinqToSql
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=BarkerApp")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bark> Barks { get; set; }
        public DbSet<UserFriend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>()
                .HasKey(f => new { f.UserId, f.FriendId });

            modelBuilder.Entity<UserFriend>()
                .HasRequired(f => f.User)
                .WithMany(c => c.Friends)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<UserFriend>()
                .HasRequired(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .WillCascadeOnDelete(false);  // <- Important
        }
    }
}
