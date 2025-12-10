using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Infrastructure.Models;
using System.Threading.Tasks;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _service;

        public OrderItemController(OrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllOrderItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetOrderItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Create([FromBody]OrderItem item)
        {
            await _service.AddOrderItemAsync(item);
            return Ok(item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Update(int id, OrderItem item)
        {
            if (id != item.OrderItemId) return BadRequest();
            await _service.UpdateOrderItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetOrderItemByIdAsync(id);
            if (item == null) return NotFound();
            await _service.DeleteOrderItemAsync(item);
            return NoContent();
        }
    }
}
