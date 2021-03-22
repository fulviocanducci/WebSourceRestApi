using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSourceRestApi.Models;
using WebSourceRestApi.Repositories;

namespace WebSourceRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class TodoController : ControllerBase
    {
        public TodoRepositoryAbstract Repository { get; }

        public TodoController(TodoRepositoryAbstract repository)
        {
            Repository = repository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> Get()
        {
            return Ok(await Repository.GetAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodo(int id)
        {
            var model = await Repository.GetAsync(id);
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
