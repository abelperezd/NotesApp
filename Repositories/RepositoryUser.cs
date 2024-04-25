using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Repositories
{
	public interface IRepositoryUser
	{
		Task<int> Create(User user);
		Task<User> GetById(int id);
		Task<User> GetByEmail(string email);
		Task<User> GetByUserName(string userName);
	}

	public class RepositoryUser : IRepositoryUser
	{
		private readonly AppDbContext _context;

		public RepositoryUser(AppDbContext context)
		{
			_context = context;
		}

		public async Task<int> Create(User user)
		{
			_context.User.Add(user);
			await _context.SaveChangesAsync();
			return user.Id;
		}

		public async Task<User> GetByEmail(string email)
		{
			email = email.ToLower();
			return await _context.User.Where(item => item.Email.ToLower().Equals(email)).FirstOrDefaultAsync();

		}

		public async Task<User> GetById(int id)
		{
			return await _context.User.Where(item => item.Id == id).FirstOrDefaultAsync();
		}

		public async Task<User> GetByUserName(string userName)
		{
			userName = userName.ToLower();
			return await _context.User.Where(item => item.UserName.ToLower().Equals(userName)).FirstOrDefaultAsync();
		}
	}
}
