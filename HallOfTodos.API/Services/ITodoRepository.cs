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

        void CreateTodo(TodoEntity todo);

        void DeleteTodo(TodoEntity todo);

        IEnumerable<TodoNote> GetTodoNotes(Guid todoId);

        TodoNote GetSingleTodoNote(Guid todoId, Guid id);

        bool TodoExists(Guid todoId);

        void AddNoteToTodo(Guid todoId, TodoNote todoNote);

        bool Save();

        void DeleteTodoNote(TodoNote todoNote);
    }
}
