using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Infrastructure.Data;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems
            .Include(m => m.Category)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MenuItem> CreateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);

        await _context.SaveChangesAsync();

        return menuItem;
    }

    public async Task<bool> UpdateAsync(MenuItem menuItem)
    {
        var existingItem = await _context.MenuItems
            .FindAsync(menuItem.Id);

        if (existingItem == null)
        {
            return false;
        }

        existingItem.Name = menuItem.Name;
        existingItem.Description = menuItem.Description;
        existingItem.Price = menuItem.Price;
        existingItem.IsAvailable = menuItem.IsAvailable;
        existingItem.ImageUrl = menuItem.ImageUrl;
        existingItem.CategoryId = menuItem.CategoryId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
        {
            return false;
        }

        _context.MenuItems.Remove(menuItem);

        await _context.SaveChangesAsync();

        return true;
    }
}