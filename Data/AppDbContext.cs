using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<NoteLike> NoteLike => Set<NoteLike>();
        public DbSet<Note> Note => Set<Note>();
        public DbSet<User> User => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //make the UserName unique
            builder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
        }
    }
}
