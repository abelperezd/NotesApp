using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Repositories
{
	public interface IRepositoryNoteLike
	{
		Task Create([Bind(new[] { "NoteId, UserId" })] NoteLike like);
		Task<bool> Exists(int noteId, int userId);
		Task<List<NoteLike>> GetLikesByNoteId(int userId);
	}

	public class RepositotyNoteLike : IRepositoryNoteLike
	{
		private readonly AppDbContext _context;

		public RepositotyNoteLike(AppDbContext context)
		{
			_context = context;
		}

		//Not needed, but for learning pourposes
		public async Task<bool> Exists(int noteId, int userId)
		{
			return await _context.NoteLike.FirstOrDefaultAsync(item => item.NoteId == noteId && item.UserId == userId) != default;
		}


		[ValidateAntiForgeryToken]
		public async Task Create([Bind("NoteId, UserId")] NoteLike like)
		{
			_context.Add(like);
			await _context.SaveChangesAsync();
		}

		public async Task<List<NoteLike>> GetLikesByNoteId(int noteId)
		{
			return _context.NoteLike.Where(item => item.NoteId == noteId).ToList();
		}

	}
}
