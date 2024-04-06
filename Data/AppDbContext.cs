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

			builder.Entity<User>().HasData(
				new { Id = 1, Name = "Abel", UserName = "abel99", RegisterDate = new DateOnly(2010, 10, 04) },
				new { Id = 2, Name = "David", UserName = "david00", RegisterDate = new DateOnly(2011, 11, 07) },
				new { Id = 3, Name = "Maria", UserName = "mar.ia15", RegisterDate = new DateOnly(2012, 08, 14) }
			);
			builder.Entity<Note>().HasData(
				new { Id = 1, Text = "First note", UserId = 1, CreationDate = new DateTime(2010, 10, 4, 10, 30, 0) },
				new { Id = 2, Text = "Second note!", UserId = 1, CreationDate = new DateTime(2010, 10, 5, 15, 32, 1) },
				new { Id = 3, Text = "Second note!", UserId = 3, CreationDate = new DateTime(2011, 08, 03, 21, 16, 2) }
			);

			builder.Entity<NoteLike>().HasData(
				new { Id = 1, NoteId = 1, UserId = 1 },
				new { Id = 2, NoteId = 1, UserId = 2 },
				new { Id = 3, NoteId = 1, UserId = 3 },
				new { Id = 4, NoteId = 2, UserId = 3 }
			);

		}
	}
}
