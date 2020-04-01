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
    public class TodoNoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Details { get; set; }
        public string WrittenBy { get; set; }
        public TodoEntity Todo { get; set; }
        public Guid TodoId { get; set; }
    }
}
