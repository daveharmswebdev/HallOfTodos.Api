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

        [HttpGet("{noteId}")]
        public IActionResult GetTodoNote(Guid todoId, Guid noteId)
        {
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("City Id does not exist");

            var note = todo.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null)
                return NotFound("Note Id does not exit.");

            return Ok(note);
        }
    }
}
