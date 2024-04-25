using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class UserRegiserDto
	{
		[Required]
		[MinLength(4)]
		[MaxLength(20)]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

		[Required]
		[MinLength(4)]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
