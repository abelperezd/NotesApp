namespace Notes.Models
{
	public class SetLikeResponseDto
	{
		public List<NoteLike> NoteLikes { get; set; }

		public bool Liked { get; set; }
	}
}
