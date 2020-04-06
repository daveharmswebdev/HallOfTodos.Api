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

        [HttpGet("{superBeingId}")]
        public IActionResult GetPowersBySuperBeingId(int superBeingId)
        {
            var powers = _superBeingRepository.GetPowers(superBeingId);

            return Ok(powers);
        }

        [HttpPost("{superBeingId}")]
        public IActionResult CreateSuperPower(int superBeingId, [FromBody] SuperBeingPowerCreateUpdateDto createDto)
        {

            var newPower = _superBeingRepository.CreatePower(superBeingId, createDto);
            return NoContent();
        }
    }
}
