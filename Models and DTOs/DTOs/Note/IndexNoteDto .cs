using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Validations;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    //TODO: the Text verification should check only if the note exists and is not itself.
    public class IndexNoteDto
    {
        public int UserId { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}
