using HallOfTodos.API.Contexts;
using HallOfTodos.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddNoteToTodo(Guid todoId, TodoNote todoNote)
        {
            var todo = GetTodo(todoId);
            todo.Notes.Add(todoNote);
        }

        public void CreateTodo(TodoEntity todo)
        {
            _context.Add(todo);
        }

        public void DeleteTodo(TodoEntity todo)
        {
            _context.Remove(todo);
        }

        public void DeleteTodoNote(TodoNote todoNote)
        {
            _context.TodoNotes.Remove(todoNote);
        }

        public TodoNote GetSingleTodoNote(Guid todoId, Guid id)
        {
            return _context.TodoNotes.Where(n => n.TodoId == todoId && n.Id == id).FirstOrDefault();
        }

        public TodoEntity GetTodo(Guid todoId, bool includeNotes = false)
        {
            return includeNotes
                ? _context.Todos.Where(t => t.Id == todoId).Include(t => t.Notes).FirstOrDefault()
                : _context.Todos.Where(t => t.Id == todoId).FirstOrDefault();
        }

        public IEnumerable<TodoNote> GetTodoNotes(Guid todoId)
        {
            return _context.TodoNotes.Where(n => n.TodoId == todoId).ToList();
        }

        public IEnumerable<TodoEntity> GetTodos()
        {
            var todos = _context.Todos.Include(t => t.Notes).OrderBy(t => t.Todo).ToList();
            return todos;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool TodoExists(Guid todoId)
        {
            return _context.Todos.Any(t => t.Id == todoId);
        }
    }
}
