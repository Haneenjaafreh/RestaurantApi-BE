using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Infrastructure.Models;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderService.AddOrderAsync(order);
            return Ok(order);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.OrderId) return BadRequest();
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            await _orderService.DeleteOrderAsync(order);
            return NoContent();
        }
    }
}
