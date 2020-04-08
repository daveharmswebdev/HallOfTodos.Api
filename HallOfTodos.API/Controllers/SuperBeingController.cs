using AutoMapper;
using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using HallOfTodos.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SuperBeingController> _logger;

        public SuperBeingController(
            ISuperBeingRepository superBeingRepository,
            IMapper mapper,
            ILogger<SuperBeingController> logger)
        {
            _superBeingRepository = superBeingRepository ?? throw new ArgumentNullException(nameof(superBeingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public IActionResult GetSuperBeings()
        {
            try
            {
                var superBeingEntities = _superBeingRepository.GetSuperBeings();
                var superBeings = _mapper.Map<IList<SuperBeingDto>>(superBeingEntities);
                return Ok(superBeings);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Attempted to get superBeings", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
        }


        [HttpGet("{superBeingId}", Name = "GetSuperBeingById")]
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
            catch (Exception ex)
            {
                _logger.LogCritical($"Attempted to get superBeing {superBeingId}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpPost]
        public IActionResult CreateSuperBeing([FromBody] SuperBeingCreateDto createDto)
        {
            try
            {
                var superBeing = _mapper.Map<SuperBeing>(createDto);
                var createdSuperBeing = _superBeingRepository.CreateSuperBeing(superBeing);
                var superBeingDto = _mapper.Map<SuperBeingDto>(createdSuperBeing); 
                return CreatedAtRoute("GetSuperBeingById", new { superBeingId = superBeingDto.Id }, superBeingDto);

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exceptions while creating SuperBeing.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpDelete("{superBeingId}")]
        public IActionResult DeleteSuperBeing(int superBeingId)
        {
            try
            {
                if (!_superBeingRepository.SuperBeingExists(superBeingId))
                    return BadRequest($"SuperBeing {superBeingId} does not exist.");

                _superBeingRepository.DeleteSuperBeing(superBeingId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exceptions while deleting SuperBeing.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

    }
}
