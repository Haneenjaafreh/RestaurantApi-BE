using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Infrastructure.Models;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
    
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
   
        public async Task<IActionResult> Create(Employee employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return Ok(employee);
        }

        [HttpPut("{id}")]
     
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.EmployeeId) return BadRequest();
            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            await _employeeService.DeleteEmployeeAsync(employee);
            return NoContent();
        }
    }
}
