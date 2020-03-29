using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Models
{
    public class TodosDto
    {
        public Guid Id { get; set; }
        public string Todo { get; set; }
        public string Doing { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public ICollection<TodoNotesDto> Notes { get; set; }
            = new List<TodoNotesDto>();
    }
}
