using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;

		public UserController(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegiserDto userDto)
		{
			if (!ModelState.IsValid)
				return View(userDto);

			DateTime now = DateTime.Now;

			User user = new User()
			{
				Email = userDto.Email,
				UserName = userDto.UserName,
				Name = userDto.Name,
				RegisterDate = new DateOnly(now.Year, now.Month, now.Day)
			};

			IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Notes");
			}
			else
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
				return View(userDto);
			}

		}

	}
}
