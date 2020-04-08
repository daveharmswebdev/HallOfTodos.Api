using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Models
{
    public class SuperBeingCreateDto
    {
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
