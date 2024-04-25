using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Data
{
	public class AppDbContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<NoteLike> NoteLike => Set<NoteLike>();
		public DbSet<Note> Note => Set<Note>();
		public DbSet<User> User => Set<User>();
		public DbSet<NoteImportance> NoteImportance => Set<NoteImportance>();

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//make the UserName unique
			builder.Entity<User>().HasIndex(x => x.UserName).IsUnique();

			SeedData(builder);
		}

		private static void SeedData(ModelBuilder builder)
		{
			builder.Entity<NoteImportance>().HasData(
				new { Id = 1, Importance = "Low" },
				new { Id = 2, Importance = "Medium" },
				new { Id = 3, Importance = "High" }
			);

			builder.Entity<User>().HasData(
				new { Id = 1, Name = "Abel", UserName = "abel99", Email = "abp@gmail.com", PasswordHashset = "a1b2", RegisterDate = new DateOnly(2010, 10, 04) },
				new { Id = 2, Name = "David", UserName = "david00", Email = "dvd@gmail.com", PasswordHashset = "c3d4", RegisterDate = new DateOnly(2011, 11, 07) },
				new { Id = 3, Name = "Maria", UserName = "mar.ia15", Email = "mr.ia@hotmail.com", PasswordHashset = "e5d6", RegisterDate = new DateOnly(2012, 08, 14) },
				new { Id = 4, Name = "Manolo", UserName = "manolito", Email = "lolito@hotmail.com", PasswordHashset = "f7g8", RegisterDate = new DateOnly(2013, 09, 15) }
			);
			builder.Entity<Note>().HasData(
				new { Id = 1, Text = "First note", UserId = 1, NoteImportanceId = 1, CreationDate = new DateTime(2010, 10, 4, 10, 30, 0) },
				new { Id = 2, Text = "Second note!", UserId = 1, NoteImportanceId = 2, CreationDate = new DateTime(2010, 10, 5, 15, 32, 1) },
				new { Id = 3, Text = "Third note!", UserId = 3, NoteImportanceId = 3, CreationDate = new DateTime(2011, 08, 03, 21, 16, 2) }
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
