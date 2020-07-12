using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly testDbContext _context;

        public TasksController(testDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{user}/{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(string user, int id)
        {
            Employees person = _context.Employees.FirstOrDefault(x => x.Username == user);
            Projects proj = _context.Projects.FirstOrDefault(x => x.Id == id);
            var tasks = await _context.Tasks.ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            if (id != tasks.Idtask)
            {
                return BadRequest();
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.Idtask }, tasks);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tasks>> DeleteTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return tasks;
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Idtask == id);
        }
    }
}
