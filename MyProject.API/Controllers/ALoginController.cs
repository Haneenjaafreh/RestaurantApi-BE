using Microsoft.AspNetCore.Mvc;
using MyProject.Application.DTOs;
using MyProject.Application.Services;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ALoginController : ControllerBase
    {
        private readonly ALoginService _authService;

        public ALoginController(ALoginService authService)
        {
            _authService = authService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var employee = await _authService.RegisterAsync(dto);
            return Ok(employee);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null) return Unauthorized();
            return Ok(new {token=result,status=true});
        }
    }
}
