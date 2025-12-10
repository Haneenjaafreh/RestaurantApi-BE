
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;

namespace MyProject.Application.Services
{
    public class RestaurantTableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantTableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RestaurantTable>> GetAllTablesAsync()
        {
            return await _unitOfWork.Tables.GetAllAsync();
        }

        public async Task<RestaurantTable> GetTableByIdAsync(int id)
        {
            return await _unitOfWork.Tables.GetByIdAsync(id);
        }

        public async Task AddTableAsync(RestaurantTable table)
        {
            await _unitOfWork.Tables.AddAsync(table);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTableAsync(RestaurantTable table)
        {
            _unitOfWork.Tables.Update(table);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTableAsync(RestaurantTable table)
        {
            _unitOfWork.Tables.Delete(table);
            await _unitOfWork.SaveAsync();
        }
    }
}
