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
    [Route("api/todos/{todoId}/notes")]
    public class TodoNotesController : ControllerBase
    {
        private readonly ILogger<TodoNotesController> _logger;
        private readonly IMailService _localMailService;
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoNotesController(
            ILogger<TodoNotesController> logger,
            IMailService localMailService,
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetTodoNotes(Guid todoId)
        {
            try
            {
                var todo = _todoRepository.GetTodo(todoId, true);

                if (todo == null)
                {
                    _logger.LogInformation($"Todo with id {todoId} was not found when accessing todo note.");
                    return NotFound();
                }

                var todoNotes = _todoRepository.GetTodoNotes(todoId);
                var notes = _mapper.Map<IEnumerable<TodoNoteDto>>(todoNotes);

                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting todo notes for todo with the id {todoId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpGet("{noteId}", Name = "GetTodoNote")]
        public IActionResult GetTodoNote(Guid todoId, Guid noteId)
        {
            var todo = _todoRepository.GetTodo(todoId, true);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            var noteEntity = _todoRepository.GetSingleTodoNote(todoId, noteId);

            if (noteEntity == null)
                return NotFound("Note Id does not exit.");

            var note = _mapper.Map<TodoNoteDto>(noteEntity);

            return Ok(note);
        }

        [HttpPost]
        public IActionResult CreateTodoNote(Guid todoId,
            [FromBody] TodoNoteCreateDto todoNoteCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_todoRepository.TodoExists(todoId))
                return NotFound("Todo Id does not exist");

            var todoNoteEntity = _mapper.Map<TodoNote>(todoNoteCreate);

            _todoRepository.AddNoteToTodo(todoId, todoNoteEntity);

            _todoRepository.Save();

            var noteDtoToReturn = _mapper.Map<TodoNoteDto>(todoNoteEntity);

            return CreatedAtRoute("GetTodoNote", new { todoId, noteId = noteDtoToReturn.Id }, noteDtoToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoNote(Guid todoId, Guid id,
            [FromBody] TodoNoteUpdateDto todoNoteUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check for city
            if (!_todoRepository.TodoExists(todoId))
                return NotFound($"Todo with id {todoId} does not exist");

            // check for note
            var todoNoteEntity = _todoRepository.GetSingleTodoNote(todoId, id);
            if (todoNoteEntity == null)
                return NotFound();

            _mapper.Map(todoNoteUpdate, todoNoteEntity);

            _todoRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateTodoNote(Guid todoId, Guid id,
            [FromBody] JsonPatchDocument<TodoNoteUpdateDto> patchDoc)
        {
            // check for city
            if (!_todoRepository.TodoExists(todoId))
                return NotFound($"Todo with id {todoId} does not exist");

            // check for note
            var todoNoteEntity = _todoRepository.GetSingleTodoNote(todoId, id);
            if (todoNoteEntity == null)
                return NotFound();

            var todoNoteToPatch = _mapper.Map<TodoNoteUpdateDto>(todoNoteEntity);

            patchDoc.ApplyTo(todoNoteToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            if (!TryValidateModel(todoNoteToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(todoNoteToPatch, todoNoteEntity);

            _todoRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoNote(Guid todoId, Guid id)
        {
            try
            {
                if (!_todoRepository.TodoExists(todoId))
                {
                    _logger.LogInformation($"Todo with id {todoId} was not found when trying to delete note {id}.");
                    return NotFound($"Todo with id {todoId} does not exist");
                }

                // check for note
                var todoNoteEntity = _todoRepository.GetSingleTodoNote(todoId, id);
                if (todoNoteEntity == null)
                {
                    _logger.LogInformation($"Note with id {id} was not found while trying to delete.");
                    return NotFound($"Note with id {id} does not exist");
                }


                // check for city
                _todoRepository.DeleteTodoNote(todoNoteEntity);

                _todoRepository.Save();

                _localMailService.Send($"TodoNote {id} of Todo {todoId} Deletion.", $"TodoNote {id} of Todo {todoId} Deletion. If this was a mistake please address accordingly");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting todo notes for todo with the id {todoId}.", ex);
                return StatusCode(500, "A problem happened while handling your delete request.");
            }


        }
    }
}
