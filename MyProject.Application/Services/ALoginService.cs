using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyProject.Application.DTOs;
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace MyProject.Application.Services
{
    public class ALoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Employee> _hasher;

        public ALoginService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _hasher = new PasswordHasher<Employee>();
        }

        // تسجيل مستخدم جديد
        public async Task<Employee> RegisterAsync(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Email and Password are required.");

            var exists = await _unitOfWork.Employees
                .AnyAsync(x => x.Email == dto.Email);

            if (exists)
                throw new Exception("This email is already in use.");

            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Position = "Customer"  // الدور الافتراضي
            };

            // تشفير الباسورد قبل الحفظ
            employee.Password = _hasher.HashPassword(employee, dto.Password);

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveAsync();

            return employee;
        }

        // تسجيل الدخول
        public async Task<string> LoginAsync(LoginDto dto)
        {
            // جلب جميع الموظفين
            var allEmployees = await _unitOfWork.Employees.GetAllAsync();

            // البحث عن الموظف حسب الايميل
            var employee = allEmployees.FirstOrDefault(e => e.Email == dto.Email);
            if (employee == null)
                return null;

            // إنشاء PasswordHasher للتحقق من الباسورد
            var passwordHasher = new PasswordHasher<Employee>();
            var verificationResult = passwordHasher.VerifyHashedPassword(employee, employee.Password, dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
                return null;

            // إنشاء التوكن إذا كان الباسورد صحيح
            return GenerateToken(employee);
        }


        // إنشاء التوكن
        public string GenerateToken(Employee employee)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim(ClaimTypes.Role, employee.Position)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
