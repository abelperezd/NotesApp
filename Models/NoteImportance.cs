using Microsoft.AspNetCore.Mvc;
using Notes.Validations;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	/// <summary>
	/// Dummy for learning pourposes. It tells us how important a note is. Values
	/// Are static in the database.
	/// </summary>
	public class NoteImportance
	{
		public int Id { get; set; }

		public string Importance { get; set; }
	}
}
