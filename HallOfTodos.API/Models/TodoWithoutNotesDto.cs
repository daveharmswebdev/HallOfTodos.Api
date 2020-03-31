using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Models
{
    public class TodoWithoutNotesDto
    {
        public Guid Id { get; set; }
        public string Todo { get; set; }
        public string Doing { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
