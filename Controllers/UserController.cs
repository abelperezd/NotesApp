using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Sign in


		public IActionResult Signin()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Signin(UserSignInDto userDto)
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
				await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Notes");
			}
			else
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
				return View(userDto);
			}

		}

		#endregion

		[HttpGet]
		#region Log in
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> Login(UserLogInDto dto)
		{
			if(!ModelState.IsValid) 
				return View(dto);

			Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Notes");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Wrong email or password.");
				return View(dto);
			}
		}

		#endregion

		#region Log out

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return RedirectToAction("Index", "Notes");
		}

		#endregion
	}
}
