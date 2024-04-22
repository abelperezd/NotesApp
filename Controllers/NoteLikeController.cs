using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Models;
using Notes.Repositories;
using Notes.Services;

namespace Notes.Controllers
{
	public class NoteLikeController : Controller
	{
		private const string NOT_FOUND_REDIRECT = "NotFound";

		private readonly IUserService _userService;
		private readonly IRepositoryNoteLike _repositoryNoteImportance;
		private readonly IMapper _mapper;

		public NoteLikeController(IUserService userService, IRepositoryNoteLike repositoryNoteImportance)
		{
			_userService = userService;
			_repositoryNoteImportance = repositoryNoteImportance;
		}

		#region Set and remove likes

		[HttpPost]
		public async Task<IActionResult> SetLike([FromBody] SetLikeDto data)
		{
			NoteLike noteLike = new NoteLike(data.UserId, data.NoteId);

			if (await _repositoryNoteImportance.Exists(noteLike.NoteId, noteLike.UserId))
				return BadRequest("Like already exists");

			await _repositoryNoteImportance.Create(noteLike);

			SetLikeResponseDto answer = new SetLikeResponseDto();
			//TODO: a count would be enough?
			answer.NoteLikes = await _repositoryNoteImportance.GetLikesByNoteId(noteLike.NoteId);

			return Ok(await _repositoryNoteImportance.GetLikesByNoteId(noteLike.NoteId));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveLike([FromBody] SetLikeDto data)
		{
			NoteLike noteLike = new NoteLike(data.UserId, data.NoteId);

			if (!await _repositoryNoteImportance.Exists(noteLike.NoteId, noteLike.UserId))
				return BadRequest("Like does not exist");

			await _repositoryNoteImportance.Delete(noteLike);

			SetLikeResponseDto answer = new SetLikeResponseDto();
			//TODO: a count would be enough?
			answer.NoteLikes = await _repositoryNoteImportance.GetLikesByNoteId(noteLike.NoteId);

			return Ok(await _repositoryNoteImportance.GetLikesByNoteId(noteLike.NoteId));
		}

		#endregion

		#region Delete

		/*
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
        */

		#endregion
	}
}
