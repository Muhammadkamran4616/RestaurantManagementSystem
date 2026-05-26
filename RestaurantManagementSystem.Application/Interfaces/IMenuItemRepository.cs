using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync();

    Task<MenuItem?> GetByIdAsync(int id);

    Task<MenuItem> CreateAsync(MenuItem menuItem);

    Task<bool> UpdateAsync(MenuItem menuItem);

    Task<bool> DeleteAsync(int id);
}