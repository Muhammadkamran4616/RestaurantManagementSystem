using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class CategoryService : ICategoryService
{
    public Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}