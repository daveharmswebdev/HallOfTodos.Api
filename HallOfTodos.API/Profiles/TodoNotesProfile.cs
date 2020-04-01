using AutoMapper;
using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Profiles
{
    public class TodoNotesProfile : Profile
    {
        public TodoNotesProfile()
        {
            // create
            CreateMap<TodoNoteCreateDto, TodoNoteEntity>();

            // read
            CreateMap<TodoNoteEntity, TodoNoteDto>();
        }
    }
}
