using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Models
{
    public class TodoNotesDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
