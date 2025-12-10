
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;

namespace MyProject.Application.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.SaveAsync();
        }
    }
}
