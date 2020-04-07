using AutoMapper;
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
        private readonly IMapper _mapper;

        public SuperBeingController(ISuperBeingRepository superBeingRepository, IMapper mapper)
        {
            _superBeingRepository = superBeingRepository ?? throw new ArgumentNullException(nameof(superBeingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{superBeingId}")]
        public IActionResult GetSuperBeingById(int superBeingId)
        {
            try
            {
                var superBeing = _superBeingRepository.GetSuperBeingById(superBeingId);

                if (superBeing == null)
                {
                    return NotFound();
                }

                var superBeingDto = _mapper.Map<SuperBeingDto>(superBeing);

                return Ok(superBeingDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

    }
}
