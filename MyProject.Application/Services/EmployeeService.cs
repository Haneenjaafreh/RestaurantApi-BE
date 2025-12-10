
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;

namespace MyProject.Application.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _unitOfWork.Employees.GetByIdAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.SaveAsync();
        }
    }
}
