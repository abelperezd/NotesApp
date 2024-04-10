using Microsoft.AspNetCore.Mvc;
using Notes.Models;
using Notes.Repositories;
using Notes.Services;

namespace Notes.Controllers
{
	public class NotesController : Controller
	{
		private const string NOT_FOUND_REDIRECT = "NotFound";

		private readonly IRepositoryNotes _repositoryNotes;
		private readonly IUserService _userService;

		public NotesController(IRepositoryNotes repositoryNotes, IUserService userService)
		{
			_repositoryNotes = repositoryNotes;
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			int userId = _userService.GetUserId();

			var notes = await _repositoryNotes.GetAll(userId);

			return View(notes);
		}

		#region Create

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Note note)
		{
			if (!ModelState.IsValid)
			{
				return View(note);
			}

			note.CreationDate = DateTime.Now;
			note.UserId = _userService.GetUserId();

			//check if the user already has this note
			if (await _repositoryNotes.Exists(note.Text, note.UserId))
			{
				ModelState.AddModelError(nameof(note.Text), $"Text {note.Text} already exists.");
				return View(note);
			}


			await _repositoryNotes.Create(note);

			return RedirectToAction("Index");
		}

		#endregion

		#region Edit

		[HttpGet]
		public async Task<ActionResult> Edit(int id)
		{
			int userId = _userService.GetUserId();
			Note note = await _repositoryNotes.GetById(id, userId);

			if(note == null)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			return View(note);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Note note)
		{
			int userId = _userService.GetUserId();
			bool exists = await _repositoryNotes.GetById(note.Id, userId) != null;

			if (!exists)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			await _repositoryNotes.Update(note);

			return RedirectToAction("Index");
		}


		#endregion

		#region Delete

		public async Task<IActionResult> Delete(int id)
		{
			int userId = _userService.GetUserId();

			Note note = await _repositoryNotes.GetById(id, userId);

			if (note == null)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			return View(note);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteNote(int id)
		{
			int userId = _userService.GetUserId();

			Note note = await _repositoryNotes.GetById(id, userId);

			if (note == null)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			await _repositoryNotes.Delete(id);

			return RedirectToAction("Index");
		}


		#endregion

		#region Other

		[HttpGet]
		public async Task<IActionResult> VerifyIfNoteAlreadyExist(string text)
		{
			bool noteAlreadyExist = await _repositoryNotes.Exists(text, _userService.GetUserId());

			return noteAlreadyExist
				? Json($"Note {text} already exists.")
				: Json(true);
		}

		#endregion
	}
}
