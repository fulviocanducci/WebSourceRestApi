using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSourceRestApi.Models;
using WebSourceRestApi.Repositories;

namespace WebSourceRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public TodoController(TodoRepositoryAbstract repository)
        {
            Repository = repository;
        }

        public TodoRepositoryAbstract Repository { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Repository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            return Ok(await Repository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await Repository.InsertAsync(todo);
                return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Todo todo)
        {
            if (id != todo.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                if (await Repository.AnyAsync(w => w.Id == id))
                {
                    if (await Repository.UpdateAsync(todo))
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await Repository.AnyAsync(w => w.Id == id))
            {
                if (await Repository.DeleteAsync(id))
                {
                    return NoContent();
                }
            }
            else
            {
                return NotFound();
            }
            return BadRequest();
        }
    }
}
