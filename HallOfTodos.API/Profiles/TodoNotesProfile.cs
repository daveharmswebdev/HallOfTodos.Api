using AutoMapper;

namespace HallOfTodos.API.Profiles
{
    public class TodoNotesProfile : Profile
    {
        public TodoNotesProfile()
        {
            // create
            CreateMap<Models.TodoNoteCreateDto, Entities.TodoNote>();

            // read
            CreateMap<Entities.TodoNote, Models.TodoNoteDto>();

            // updated
            CreateMap<Models.TodoNoteUpdateDto, Entities.TodoNote>().ReverseMap();
        }
    }
}
