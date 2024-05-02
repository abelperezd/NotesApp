using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;
using System.Security.Claims;

namespace Notes.Services
{
	public interface IUserService
	{
		int GetUserId();
	}

	public class UserService : IUserService
	{
		private readonly AppDbContext _context;
		private readonly HttpContext _httpContext;

		public UserService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContext = httpContextAccessor.HttpContext;
		}

		public int GetUserId()
		{
			if (_httpContext.User.Identity.IsAuthenticated)
			{
				Claim? idClaim = _httpContext.User.Claims
					.Where(item => item.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
				
				int.TryParse(idClaim.Value, out int userId);

				return userId;
			}
			else
			{
				return -1;
			}
		}

		public async Task<string> GetUserName(int userId)
		{
			User user = await _context.User.FirstOrDefaultAsync(item => item.Id == userId);

			return user != null ? user.UserName : null;

		}
	}
}
