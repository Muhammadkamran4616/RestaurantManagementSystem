using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;
    public MenuItemService(IMenuItemRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<MenuItem> CreateAsync(MenuItem menuItem)
    {
        return await _repository.CreateAsync(menuItem);
    }
    public async Task<bool> UpdateAsync(MenuItem menuItem)
    {
        return await _repository.UpdateAsync(menuItem);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}