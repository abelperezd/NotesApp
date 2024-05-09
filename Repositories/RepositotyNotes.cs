using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Repositories
{
	public interface IRepositoryNotes
	{
		Task Create([Bind(["Text, UserId, CreationDate"])] Note note);
		Task Delete(int id);
		Task<bool> Exists(string text, int userId);
		Task<bool> ExistsAndIsNotItself(string text, int userId, int id);
		Task<IEnumerable<Note>> GetAll();
		Task<IEnumerable<Note>> GetAllFromUserId(int userId);
		Task<Note> GetById(int id, int userId);
		Task Update(Note note);
	}
	public class RepositotyNotes : IRepositoryNotes
	{
		private readonly AppDbContext _context;

		public RepositotyNotes(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task Create([Bind("Text, UserId, CreationDate")] Note note)
		{
			_context.Note.Add(note);
			await _context.SaveChangesAsync();
		}


		/// <summary>
		/// Doesn't make much sense, but for learning pourposes. It returns if the user already have an equal note.
		/// </summary>
		public async Task<bool> Exists(string text, int userId)
		{
			return await _context.Note.FirstOrDefaultAsync(item => item.Text.ToLower().Equals(text.ToLower()) && item.UserId == userId) != default;
		}

		/// <summary>
		/// Doesn't make much sense, but for learning pourposes. It returns if the user already have an equal note.
		/// </summary>
		public async Task<bool> ExistsAndIsNotItself(string text, int userId, int id)
		{
			return await _context.Note.FirstOrDefaultAsync(item => item.Text.ToLower().Equals(text.ToLower()) && item.UserId == userId && item.Id != id) != default;
		}

		public async Task<IEnumerable<Note>> GetAll()
		{
			return _context.Note
				.Include(item => item.Likes)
				.Include(item => item.NoteImportance)
				.Include(item => item.User);
			//.Where(item => item.UserId == userId);
		}

		public async Task<IEnumerable<Note>> GetAllFromUserId(int userId)
		{
			return _context.Note
				.Where(item => item.UserId == userId)
				.Include(item => item.Likes)
				.Include(item => item.NoteImportance)
				.Include(item => item.User);
		}


		public async Task Update(Note note)
		{
			var dbNote = _context.Note.Single(n => n.Id == note.Id);
			dbNote.Text = note.Text;

			//alternative method
			//_context.Database.ExecuteSql($"UPDATE [Note] SET [Text] = {note.Text} WHERE [Id] = {note.Id}");

			await _context.SaveChangesAsync();
		}

		public async Task<Note> GetById(int id, int userId)
		{
			return await _context.Note.Where(item => item.Id == id && item.UserId == userId).FirstOrDefaultAsync();
		}

		public async Task Delete(int id)
		{
			Note dbNote = await _context.Note.SingleOrDefaultAsync(n => n.Id == id);

			if (dbNote != null)
			{
				_context.Note.Remove(dbNote);
				await _context.SaveChangesAsync();

			}


		}
	}
}
