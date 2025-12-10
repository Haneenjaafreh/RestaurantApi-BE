using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Application.Services
{
    public class OrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // جلب كل OrderItems
        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _unitOfWork.OrderItems.GetAllAsync();
        }

        // جلب OrderItem محدد بالمعرّف
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _unitOfWork.OrderItems.GetByIdAsync(id);
        }

        // إضافة OrderItem جديد
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _unitOfWork.OrderItems.AddAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }

        // تعديل OrderItem موجود
        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            _unitOfWork.OrderItems.Update(orderItem);
            await _unitOfWork.SaveAsync();
        }

        // حذف OrderItem
        public async Task DeleteOrderItemAsync(OrderItem orderItem)
        {
            _unitOfWork.OrderItems.Delete(orderItem);
            await _unitOfWork.SaveAsync();
        }
    }
}
