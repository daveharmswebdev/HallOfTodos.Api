﻿using HallOfTodos.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{id}")]
        public IActionResult UpdateTodoNote(Guid todoId, Guid id,
            [FromBody] TodoNoteUpdateDto todoNoteUpdate)
        {
            // check for city
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            // check for note
            var note = todo.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
                return NotFound("Note Id does not exist");

            note.Title = todoNoteUpdate.Title;
            note.Details = todoNoteUpdate.Details;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateTodoNote(Guid todoId, Guid id,
            [FromBody] JsonPatchDocument<TodoNoteUpdateDto> patchDoc)
        {
            // check for city
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            // check for note
            var note = todo.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
                return NotFound("Note Id does not exist");

            var todoNoteToPatch = new TodoNoteUpdateDto()
            {
                Title = note.Title,
                Details = note.Details
            };

            patchDoc.ApplyTo(todoNoteToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            if (!TryValidateModel(todoNoteToPatch))
            {
                return BadRequest(ModelState);
            }

            note.Title = todoNoteToPatch.Title;
            note.Details = todoNoteToPatch.Details;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoNote(Guid todoId, Guid id)
        {
            // check for city
            var todo = TodosDataStore.Current.Todos
                .FirstOrDefault(t => t.Id == todoId);

            if (todo == null)
                return NotFound("Todo Id does not exist");

            // check for note
            var note = todo.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
                return NotFound("Note Id does not exist");

            todo.Notes.Remove(note);

            return NoContent();
        }
    }
}
