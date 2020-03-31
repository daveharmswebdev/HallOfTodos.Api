using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Entities
{
    [JsonObject(IsReference = true)]
    public class TodoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Todo { get; set; }
        public string Doing { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public ICollection<TodoNoteEntity> Notes { get; set; }
            = new List<TodoNoteEntity>();
    }
}
