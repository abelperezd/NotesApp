using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
	public class User
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[MinLength(4)]
		[MaxLength(50)]
		public string UserName { get; set; }


		[Required]
		[MinLength(4)]
		[MaxLength(20)]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string PasswordHash{ get; set; }
		
		[Required]
		public DateOnly RegisterDate { get; set; }


	}
}
