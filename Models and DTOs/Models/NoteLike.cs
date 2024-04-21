namespace Notes.Models
{
    public class NoteLike
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public Note? Note { get; set; }
        public User? User { get; set; }

        public NoteLike()
        {
            
        }
        public NoteLike(int userId, int noteId)
        {
            UserId = userId;
            NoteId = noteId;
        }

    }
}
