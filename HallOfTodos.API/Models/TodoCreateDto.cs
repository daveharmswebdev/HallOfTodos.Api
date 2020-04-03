using System;
using System.ComponentModel.DataAnnotations;

namespace HallOfTodos.API.Models
{
    public class TodoCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Todo { get; set; }
        public string Doing { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}