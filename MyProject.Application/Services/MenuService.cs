using MyProject.Application.DTOs;
using MyProject.Infrastructure.Models;
using MyProject.Infrastructure.UnitOfWork;


namespace MyProject.Application.Services
{
    public class MenuService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _unitOfWork.Menus.GetAllAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await _unitOfWork.Menus.GetByIdAsync(id);
        }

        public async Task AddMenuAsync(Menu menu)
        {
            await _unitOfWork.Menus.AddAsync(menu);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _unitOfWork.Menus.Update(menu);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMenuAsync(Menu menu)
        {
            _unitOfWork.Menus.Delete(menu);
            await _unitOfWork.SaveAsync();
        }
    }
}
