using HallOfTodos.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Services
{
    public interface ITodoRepository
    {
        IEnumerable<TodoEntity> GetTodos();

        TodoEntity GetTodo(Guid todoId, bool includesNotes);

        IEnumerable<TodoNoteEntity> GetTodoNotes(Guid todoId);

        TodoNoteEntity GetSingleTodoNote(Guid todoId, Guid id);

        public bool TodoExists(Guid todoId);

        public void AddNoteToTodo(Guid todoId, TodoNoteEntity todoNote);

        public bool Save();
    }
}
