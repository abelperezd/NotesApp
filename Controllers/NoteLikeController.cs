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

		#region Create

		[HttpPost]
		public async Task<IActionResult> SetLike([FromBody] LikeData data)
		{
			NoteLike noteLike = new NoteLike(data.UserId, data.NoteId);

			if (await _repositoryNoteImportance.Exists(noteLike.NoteId, noteLike.UserId))
				return BadRequest("Like already exists");

			await _repositoryNoteImportance.Create(noteLike);

			return Ok(await _repositoryNoteImportance.GetLikesByNoteId(noteLike.NoteId));
		}

		public class LikeData
		{
			public int NoteId { get; set; }
			public int UserId { get; set; }
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
