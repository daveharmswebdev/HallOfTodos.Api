using AutoMapper;
using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            // reads
            CreateMap<TodoEntity, TodoWithoutNotesDto>();
            CreateMap<TodoEntity, TodoDto>();
            CreateMap<TodoCreateDto, TodoEntity>();
        }
    }
}
