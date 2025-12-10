using Microsoft.AspNetCore.Mvc;

using MyProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyProject.Infrastructure.Models;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id) return BadRequest("Id mismatch");

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok("Updated successfully");
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("Deleted successfully");
        }



    }
}
