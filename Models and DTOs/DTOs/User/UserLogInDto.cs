using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class UserLogInDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
