using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(TodosDataStore.Current.Todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(Guid id)
        {
            var todoToReturn = TodosDataStore
                .Current.Todos.FirstOrDefault(t => t.Id == id);

            if (todoToReturn == null)
                return NotFound();

            return Ok(todoToReturn);
        }

    }
}
