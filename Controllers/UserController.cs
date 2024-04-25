using Microsoft.AspNetCore.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegiserDto userDto)
		{
			if (!ModelState.IsValid)
				return View(userDto);

			return RedirectToAction("Index", "Notes");
		}

	}
}
