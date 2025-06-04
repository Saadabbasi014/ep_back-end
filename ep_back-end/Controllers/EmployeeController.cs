using Microsoft.AspNetCore.Mvc;
using ep_back_end.Services;
using Microsoft.EntityFrameworkCore;
using ep_back_end.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ep_back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedEmployees = await _service.GetPagedAsync(pageNumber, pageSize);
            return Ok(pagedEmployees);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var employees = await _service.GetByNameAsync(name);

            if (employees == null || !employees.Any())
                return NotFound("No employees found with the provided name.");

            return Ok(employees);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            //if (id != employee.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(employee);
            if (!updated) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
