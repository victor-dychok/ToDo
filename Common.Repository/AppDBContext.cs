using Common.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDomain;

namespace Common.Repository
{
    public class AppDBContext : DbContext
    {
        public DbSet<TodoItem> ToDoItems { get; set; }

        public DbSet<User> Users { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasKey(c => c.Id);
            modelBuilder.Entity<TodoItem>().Property(b => b.Label).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(b => b.Login).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();

            modelBuilder.Entity<TodoItem>()
                .HasOne(v => v.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}
