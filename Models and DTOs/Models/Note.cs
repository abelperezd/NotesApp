using Microsoft.AspNetCore.Mvc;
using Notes.Validations;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class Note
	{
		//for Id, the key is deduced
		//[Key]
		public int Id { get; set; }

		[StringLength(maximumLength: 100, MinimumLength = 3)]
		[Required]
		[FirstLetterIsCapital]
		public string Text { get; set; }

		public int UserId { get; set; }

		public int NoteImportanceId { get; set; }

		public DateTime CreationDate { get; set; }

		#region Linked objects

		public User? User { get; set; }
		public List<NoteLike>? Likes { get; set; }

		#endregion
	}
}
