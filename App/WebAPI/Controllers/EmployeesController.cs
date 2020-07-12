using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class EmployeesController : ControllerBase
    {
        private readonly testDbContext _context;

        public EmployeesController(testDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/rol/leader
        [HttpGet("rol/{id}")]
        public async Task<ActionResult<IEnumerable<Employees>>> GetSupervisorEmployees(string id)
        {
            return await _context.Employees.Where(x => x.ComandRol == id).ToListAsync();
        }

        // GET: api/Employees/users/2/leader
        [HttpGet("users/{id}/{rol}")]
        public IActionResult GetProjectsEmployees(string id, string rol)
        {

            var employees = from pe in _context.ProjectsEmployees
                                              join p in _context.Projects on pe.IdProject equals p.Id
                                              join e in _context.Employees on pe.IdEmployee equals e.IdEmployee
                                              where p.Id == Convert.ToInt32(id) && e.ComandRol == rol
                                              select new
                                              {
                                                  e.IdEmployee,
                                                  e.NameEmployee,
                                                  e.Password,
                                                  e.PatronimicEmployee,
                                                  e.SurnameEmployee,
                                                  e.Username,
                                                  e.AuthRol,
                                                  e.ComandRol,
                                                  e.Email
                                              };

            return Ok(employees);
        }

        // GET: api/Employees/5
        //[HttpGet("Ai/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(short id)
        {
            var employees = await _context.Employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees(short id, Employees employees)
        {
            if (id != employees.IdEmployee)
            {
                return BadRequest();
            }

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var response = new
            {
                status = "Ok",
                name = "Все ок"
            };

            return Ok(response);
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(Employees employees)
        {
            _context.Employees.Add(employees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new { id = employees.IdEmployee }, employees);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> DeleteEmployees(short id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();

            return employees;
        }

        private bool EmployeesExists(short id)
        {
            return _context.Employees.Any(e => e.IdEmployee == id);
        }
    }
}
