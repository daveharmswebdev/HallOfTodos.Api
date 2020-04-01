using AutoMapper;
using HallOfTodos.API.Models;
using HallOfTodos.API.Services;
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
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodosController(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var todoEntities = _todoRepository.GetTodos();
            var results = _mapper.Map<IEnumerable<TodoWithoutNotesDto>>(todoEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(Guid id, [FromQuery] bool includeNotes = false)
        {
            var todoEntity = _todoRepository.GetTodo(id, includeNotes);

            if (todoEntity == null)
                return NotFound();

            if (includeNotes)
                return Ok(_mapper.Map<TodoDto>(todoEntity));

            return Ok(_mapper.Map<TodoWithoutNotesDto>(todoEntity));
        }

    }
}
