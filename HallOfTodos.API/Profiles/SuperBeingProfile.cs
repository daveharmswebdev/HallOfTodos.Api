using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Profiles
{
    public class SuperBeingProfile : Profile
    {
        public SuperBeingProfile()
        {
            CreateMap<Entities.SuperBeing, Models.SuperBeingDto>();
            CreateMap<Models.SuperBeingCreateDto, Entities.SuperBeing>();
        }
    }
}
