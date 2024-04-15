using AutoMapper;
using Notes.Models;

namespace Notes.Mapping
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<CreateNoteDto, Note>();
			CreateMap<Note, EditNoteDto>();
			CreateMap<EditNoteDto, Note>();
		}
	}
}
