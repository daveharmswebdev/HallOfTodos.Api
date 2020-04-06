using HallOfTodos.API.Models;
using HallOfTodos.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Controllers
{
    [ApiController]
    [Route("api/SuperBeing")]
    public class SuperBeingController : ControllerBase
    {
        private readonly ISuperBeingRepository _superBeingRepository;

        public SuperBeingController(ISuperBeingRepository superBeingRepository)
        {
            _superBeingRepository = superBeingRepository;
        }

    }
}
