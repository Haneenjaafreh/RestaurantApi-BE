
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;

namespace MyProject.Application.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.Orders.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            //var table = await _unitOfWork.Tables.GetByIdAsync(order.RestaurantTableId);
            //if (table == null)
            //    throw new Exception("Invalid TableId, table does not exist.");

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();

          
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveAsync();
        }
    }
}
