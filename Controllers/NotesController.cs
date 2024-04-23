using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		private readonly IRepositoryNoteImportance _repositoryNoteImportance;
		private readonly IMapper _mapper;

		public NotesController(IRepositoryNotes repositoryNotes, IUserService userService, IRepositoryNoteImportance noteImportance, IMapper mapper)
		{
			_repositoryNotes = repositoryNotes;
			_userService = userService;
			_repositoryNoteImportance = noteImportance;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			int userId = _userService.GetUserId();

			IndexNoteDto dto = new IndexNoteDto();
			dto.Notes = await _repositoryNotes.GetAll(userId);
			dto.UserId = _userService.GetUserId();

			return View(dto);
		}

		#region Create

		public IActionResult Create()
		{
			CreateNoteDto model = new CreateNoteDto();
			model.NoteImportance = GetNoteImportance();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateNoteDto noteDto)
		{
			if (!ModelState.IsValid)
			{
				noteDto.NoteImportance = GetNoteImportance();
				return View(noteDto);
			}

			Note note = _mapper.Map<Note>(noteDto);

			note.CreationDate = DateTime.Now;
			note.UserId = _userService.GetUserId();

			//check if the user already has this note
			if (await _repositoryNotes.Exists(note.Text, note.UserId))
			{
				noteDto.NoteImportance = GetNoteImportance();
				ModelState.AddModelError(nameof(noteDto.Text), $"Text {noteDto.Text} already exists.");
				return View(noteDto);
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

			if (note == null)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			EditNoteDto noteDto = _mapper.Map<EditNoteDto>(note);
			noteDto.NoteImportance = GetNoteImportance();

			return View(noteDto);
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
		public async Task<IActionResult> DeleteNoteFromView(int id)
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

		[HttpPost]
		public async Task<IActionResult> DeleteNoteFromToast(int id)
		{
			int userId = _userService.GetUserId();

			Note note = await _repositoryNotes.GetById(id, userId);

			if (note == null)
			{
				return BadRequest("Not authorised");
			}

			//for now, avoid removing it from the database
			return Ok();
			await _repositoryNotes.Delete(id);

			return Ok();
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

		private IEnumerable<SelectListItem> GetNoteImportance()
		{
			IEnumerable<NoteImportance> noteImportance = _repositoryNoteImportance.GetAll().Result;

			return noteImportance.Select(item => new SelectListItem(item.Importance, item.Id.ToString()));
		}

		#endregion
	}
}
