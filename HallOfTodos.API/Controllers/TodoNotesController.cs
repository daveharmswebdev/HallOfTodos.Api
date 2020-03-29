using HallOfTodos.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Controllers
{
    [ApiController]
    [Route("api/todos/{todoId}/notes")]
    public class TodoNotesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTodoNotes(Guid todoId)
        {
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound();

            var notes = todo.Notes;

            return Ok(notes);
        }

        [HttpGet("{noteId}", Name = "GetTodoNote")]
        public IActionResult GetTodoNote(Guid todoId, Guid noteId)
        {
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            var note = todo.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null)
                return NotFound("Note Id does not exit.");

            return Ok(note);
        }

        [HttpPost]
        public IActionResult CreateTodoNote(Guid todoId,
            [FromBody] TodoNoteCreateDto todoNoteCreate)
        {
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            var todoNote = new TodoNotesDto()
            {
                Id = Guid.NewGuid(),
                Title = todoNoteCreate.Title,
                Details = todoNoteCreate.Details
            };

            todo.Notes.Add(todoNote);

            return CreatedAtRoute("GetTodoNote", new { todoId, noteId = todoNote.Id }, todoNote);
        }
    }
}
