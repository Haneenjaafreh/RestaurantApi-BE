using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Infrastructure.Models;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return Ok(menu);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Create(Menu menu)
        {
            await _menuService.AddMenuAsync(menu);
            return Ok(menu);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Menu menu)
        {
            if (id != menu.MenuId) return BadRequest();
            await _menuService.UpdateMenuAsync(menu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            await _menuService.DeleteMenuAsync(menu);
            return NoContent();
        }
    }
}
