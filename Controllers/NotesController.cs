using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Models;
using Notes.Repositories;
using Notes.Services;

namespace Notes.Controllers
{
	//[Authorize] //if we want authorization for all the actions of the controller
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

		#region View

		[AllowAnonymous] //to allow unidentified users
		public async Task<IActionResult> Index(string toShow = null)
		{
			IndexNoteDto dto = new IndexNoteDto();
			dto.UserId = _userService.GetUserId();
			dto.SubMenuNotes = string.IsNullOrEmpty(toShow) ? SubMenuNotes.All : (SubMenuNotes)Enum.Parse(typeof(SubMenuNotes), toShow); ;

			if (dto.SubMenuNotes == SubMenuNotes.All)
			{
				dto.Notes = await _repositoryNotes.GetAll();
			}
			else
			{
				dto.Notes = await _repositoryNotes.GetAllFromUserId(dto.UserId);

			}

			return View(dto);
		}

		#endregion

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
		[Authorize]
		public async Task<ActionResult> Edit(int id, string toShow = null)
		{
			int userId = _userService.GetUserId();
			Note note = await _repositoryNotes.GetById(id, userId);

			if (note == null)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			EditNoteDto noteDto = _mapper.Map<EditNoteDto>(note);
			noteDto.NoteImportance = GetNoteImportance();
			noteDto.BackUrl = string.IsNullOrEmpty(toShow) ? SubMenuNotes.All : (SubMenuNotes)Enum.Parse(typeof(SubMenuNotes), toShow);
			return View(noteDto);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> Edit(Note note, string toShow = null)
		{
			int userId = _userService.GetUserId();
			bool exists = await _repositoryNotes.GetById(note.Id, userId) != null;

			if (!exists)
			{
				return RedirectToAction(NOT_FOUND_REDIRECT, "Home");
			}

			await _repositoryNotes.Update(note);

			return RedirectToAction("Index", string.IsNullOrEmpty(toShow) ? SubMenuNotes.All : (SubMenuNotes)Enum.Parse(typeof(SubMenuNotes), toShow));
		}


		#endregion

		#region Delete

		[Authorize]
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
		[Authorize]
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
		[Authorize]
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
		public async Task<IActionResult> VerifyIfNoteAlreadyExist(string text, int? id = null)
		{
			bool noteAlreadyExist = id == null
				? await _repositoryNotes.Exists(text, _userService.GetUserId())
				: await _repositoryNotes.ExistsAndIsNotItself(text, _userService.GetUserId(), id.Value);

			return noteAlreadyExist
				? Json($"Note '{text}' already exists.")
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
