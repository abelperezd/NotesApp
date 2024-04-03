using Microsoft.AspNetCore.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
    public class NotesController:Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                return View(note);
            }

            note.CreationDate = DateTime.Now;
            note.UserId = 2;

            return View();
        }
    }
}
