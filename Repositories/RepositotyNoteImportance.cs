using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Repositories
{
	public interface IRepositoryNoteImportance
	{
		Task<IEnumerable<NoteImportance>> GetAll();
	}

	public class RepositotyNoteImportance: IRepositoryNoteImportance
	{
		private readonly AppDbContext _context;

		public RepositotyNoteImportance(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<NoteImportance>> GetAll()
		{
			return _context.NoteImportance.ToList();
		}
	}
}
