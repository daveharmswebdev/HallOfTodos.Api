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

        public TodoNoteEntity GetSingleTodoNote(Guid todoId, Guid id)
        {
            return _context.TodoNotes.Where(n => n.TodoId == todoId && n.Id == id).FirstOrDefault();
        }

        public TodoEntity GetTodo(Guid todoId, bool includeNotes)
        {
            return includeNotes
                ? _context.Todos.Where(t => t.Id == todoId).Include(t => t.Notes).FirstOrDefault()
                : _context.Todos.Where(t => t.Id == todoId).FirstOrDefault();
        }

        public IEnumerable<TodoNoteEntity> GetTodoNotes(Guid todoId)
        {
            return _context.TodoNotes.Where(n => n.TodoId == todoId).ToList();
        }

        public IEnumerable<TodoEntity> GetTodos()
        {
            return _context.Todos.OrderBy(t => t.Todo).ToList();
        }
    }
}
