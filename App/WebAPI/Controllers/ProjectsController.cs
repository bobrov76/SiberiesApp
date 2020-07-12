using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly testDbContext _context;

        public ProjectsController(testDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("rol/{id}")]
        public IActionResult GetProjectsEmployees(string id, string rol)
        {
            
            Employees person = _context.Employees.FirstOrDefault(x => x.Username == id);
            var projects = from pe in _context.ProjectsEmployees
                                   join p in _context.Projects on pe.IdProject equals p.Id
                                   join e in _context.Employees on pe.IdEmployee equals e.IdEmployee
                                   where e.IdEmployee == person.IdEmployee
                                   select new
                                   {
                                       p.Id,
                                       p.NameExecutingCompany,
                                       p.NameProject,
                                       p.NameСustomerCompany,
                                       p.Priority,
                                       p.StartData,
                                       p.EndData,
                                       p.FilePath
                                   };
                    return Ok(projects);    

        }
        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projects>> GetProjects(int id)
        {
            var projects = await _context.Projects.FindAsync(id);

            if (projects == null)
            {
                return NotFound();
            }

            return projects;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjects(int id, Projects projects)
        {
            if (id != projects.Id)
            {
                return BadRequest();
            }

            _context.Entry(projects).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsExists(id))
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

        public class FileUPloadAPI
        {
            public IFormFile files { get; set; }
            public string Id { get; set; }
        }
        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Projects>> PostProjects(Projects projects)
        {
            _context.Projects.Add(projects);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjects", new { id = projects.Id }, projects);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Projects>> DeleteProjects(int id)
        {
            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();

            return projects;
        }

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
