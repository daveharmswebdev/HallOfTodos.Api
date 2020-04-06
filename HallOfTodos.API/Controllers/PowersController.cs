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
    [Route("api/SuperBeing/{superBeingId}/powers")]
    public class PowersController : ControllerBase
    {
        private readonly ISuperBeingRepository _superBeingRepository;

        public PowersController(ISuperBeingRepository superBeingRepository)
        {
            _superBeingRepository = superBeingRepository;
        }

        [HttpGet]
        public IActionResult GetPowers(int superBeingId)
        {
            var powers = _superBeingRepository.GetPowers(superBeingId);

            return Ok(powers);
        }

        [HttpGet("{powerId}")]
        public IActionResult GetPowerById(int superBeingId, int powerId)
        {
            var power = _superBeingRepository.GetPowerById(powerId);
            return Ok(power);
        }

        [HttpPost]
        public IActionResult CreateSuperPower(int superBeingId, [FromBody] SuperBeingPowerCreateUpdateDto createDto)
        {

            var newPower = _superBeingRepository.CreatePower(superBeingId, createDto);
            return NoContent();
        }
    }
}
