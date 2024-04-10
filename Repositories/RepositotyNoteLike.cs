using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Repositories
{
	public interface IRepositoryNoteLike
	{
		Task<bool> Exists(int noteId, int userId);
	}

	public class RepositotyNoteLike: IRepositoryNoteLike
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
	}
}
