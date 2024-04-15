using AutoMapper;
using Notes.Models;

namespace Notes.Mapping
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Note, NoteViewModel>();
		}
	}
}
