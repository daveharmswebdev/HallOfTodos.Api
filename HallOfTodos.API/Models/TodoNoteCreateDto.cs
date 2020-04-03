using System.ComponentModel.DataAnnotations;

namespace HallOfTodos.API.Models
{
    public class TodoNoteCreateDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Details { get; set; }
        public string WrittenBy { get; set; }
    }
}
