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
    public class ProjectsEmployeesController : ControllerBase
    {
        private readonly testDbContext _context;

        public ProjectsEmployeesController(testDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectsEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectsEmployees>>> GetProjectsEmployees()
        {
            return await _context.ProjectsEmployees.ToListAsync();
        }

        // GET: api/ProjectsEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsEmployees>> GetProjectsEmployees(int id)
        {
            var projectsEmployees = await _context.ProjectsEmployees.FindAsync(id);

            if (projectsEmployees == null)
            {
                return NotFound();
            }

            return projectsEmployees;
        }

        // PUT: api/ProjectsEmployees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectsEmployees(int id, ProjectsEmployees projectsEmployees)
        {
            if (id != projectsEmployees.IdTasksEmployees)
            {
                return BadRequest();
            }

            _context.Entry(projectsEmployees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsEmployeesExists(id))
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

        // POST: api/ProjectsEmployees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectsEmployees>> PostProjectsEmployees(ProjectsEmployees projectsEmployees)
        {
            _context.ProjectsEmployees.Add(projectsEmployees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectsEmployees", new { id = projectsEmployees.IdTasksEmployees }, projectsEmployees);
        }

        // DELETE: api/ProjectsEmployees/5
        [HttpDelete("{idUser}/{idProj}")]
        public async Task<ActionResult<ProjectsEmployees>> DeleteProjectsEmployees(int idUser, int idProj)
        {
            var projectsEmployees = _context.ProjectsEmployees.FirstOrDefault(x => x.IdProject == idProj && x.IdEmployee == idUser);
            if (projectsEmployees == null)
            {
                return NotFound();
            }

            _context.ProjectsEmployees.Remove(projectsEmployees);
            await _context.SaveChangesAsync();

            return projectsEmployees;
        }

        private bool ProjectsEmployeesExists(int id)
        {
            return _context.ProjectsEmployees.Any(e => e.IdTasksEmployees == id);
        }
    }
}
