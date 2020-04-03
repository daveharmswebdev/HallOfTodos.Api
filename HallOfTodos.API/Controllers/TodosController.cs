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
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TodosController> _logger;

        public TodosController(ITodoRepository todoRepository, IMapper mapper, ILogger<TodosController> logger)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var todoEntities = _todoRepository.GetTodos();
            var results = _mapper.Map<IEnumerable<TodoWithoutNotesDto>>(todoEntities);

            return Ok(results);
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetTodo(Guid id, [FromQuery] bool includeNotes = false)
        {
            var todoEntity = _todoRepository.GetTodo(id, includeNotes);

            if (todoEntity == null)
                return NotFound();

            if (includeNotes)
                return Ok(_mapper.Map<TodoDto>(todoEntity));

            return Ok(_mapper.Map<TodoWithoutNotesDto>(todoEntity));
        }

        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoCreateDto todoCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todoEntity = _mapper.Map<TodoEntity>(todoCreateDto);

            _todoRepository.CreateTodo(todoEntity);
            _todoRepository.Save();

            var todoToReturn = _mapper.Map<TodoDto>(todoEntity);

            return CreatedAtRoute("GetTodo", new { id = todoToReturn.Id }, todoToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(Guid id)
        {
            try
            {
                var todoEntity = _todoRepository.GetTodo(id, false);

                if (todoEntity == null)
                    return BadRequest($"Todo {id} does not exist. Nothing was deleted at this time");

                _todoRepository.DeleteTodo(todoEntity);
                _todoRepository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting todo {id}", ex);
                return StatusCode(500, "A problem occurred while trying to delete this todo");
            }
        }

    }
}
