using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Models
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Todo { get; set; }
        public string Doing { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public ICollection<TodoNoteDto> Notes { get; set; }
            = new List<TodoNoteDto>();
    }
}
