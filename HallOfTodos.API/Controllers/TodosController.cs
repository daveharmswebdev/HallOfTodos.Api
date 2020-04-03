using AutoMapper;
using HallOfTodos.API.Entities;
using HallOfTodos.API.Models;
using HallOfTodos.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

        [HttpPut("{id}")]
        public IActionResult UpdateTodo (Guid id, [FromBody] TodoUpdateDto todoUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var todoEntity = _todoRepository.GetTodo(id, false);
                if (todoEntity == null)
                    return NotFound();

                _mapper.Map(todoUpdateDto, todoEntity);

                _todoRepository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while updating todo {id}", ex);
                return StatusCode(500, "A problem occurred while trying to update this todo");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateTodo (Guid id, [FromBody] JsonPatchDocument<TodoUpdateDto> patchDoc)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var todoEntity = _todoRepository.GetTodo(id, false);
                if (todoEntity == null)
                    return NotFound();

                var todoToPatch = _mapper.Map<TodoUpdateDto>(todoEntity);

                patchDoc.ApplyTo(todoToPatch, ModelState);

                if (!ModelState.IsValid || !TryValidateModel(ModelState))
                    return BadRequest(ModelState);

                _mapper.Map(todoToPatch, todoEntity);

                _todoRepository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while updating todo {id}", ex);
                return StatusCode(500, "A problem occurred while trying to update this todo");
            }
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
