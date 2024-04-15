

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Notes.Models
{
	public class NoteViewModel : Note
	{
		public IEnumerable<SelectListItem>? NoteImportance { get; set; }

        public NoteViewModel() : base() { }
    }
}
