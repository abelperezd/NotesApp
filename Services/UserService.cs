using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Services
{
    public interface IUserService
    {
        int GetUserId();
    }

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public int GetUserId()
        {
            return 4;
        }

        public async Task<string> GetUserName(int userId)
        {
            User user = await _context.User.FirstOrDefaultAsync(item => item.Id == userId);

            return user != null ? user.UserName : null;

        }
    }
}
