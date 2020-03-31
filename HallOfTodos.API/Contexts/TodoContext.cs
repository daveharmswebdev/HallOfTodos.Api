using HallOfTodos.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<TodoNoteEntity> TodoNotes { get; set; }
    }
}
