using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Infrastructure.Models;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantTableController : ControllerBase
    {
        private readonly RestaurantTableService _tableService;

        public RestaurantTableController(RestaurantTableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null) return NotFound();
            return Ok(table);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RestaurantTable table)
        {
            await _tableService.AddTableAsync(table);
            return Ok(table);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, RestaurantTable table)
        {
            if (id != table.RestaurantTableId) return BadRequest();
            await _tableService.UpdateTableAsync(table);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null) return NotFound();
            await _tableService.DeleteTableAsync(table);
            return NoContent();
        }
    }
}
