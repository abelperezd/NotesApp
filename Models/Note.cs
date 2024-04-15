using Microsoft.AspNetCore.Mvc;
using Notes.Validations;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class Note : IValidatableObject
	{
		//for Id, the key is deduced
		//[Key]
		public int Id { get; set; }

		[StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
		[Required(ErrorMessage = "The {0} field is required")]
		[Display(Name = "Note message")]
		[FirstLetterIsCapital]
		[Remote(action: "VerifyIfNoteAlreadyExist", controller: "Notes")] //To verify real-time from the front end if a note with this text already exists
		public string Text { get; set; }

		public int UserId { get; set; }
		
		[Display(Name ="Importance")]
		public int NoteImportanceId { get; set; }
		
		public DateTime CreationDate { get; set; }
		public User? User { get; set; }
		public List<NoteLike>? Likes { get; set; }


		//This is not really needed in our case. But in other situations, it is used to do more elaborated validations.
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			//The second paramter tells us which properties have the error. If it not indicated, it is considered an error model. E.g: some user is not allowed to do something
			if (Text == null)
				yield return new ValidationResult("Field cannot be null", [nameof(Text)]);
		}
	}
}
