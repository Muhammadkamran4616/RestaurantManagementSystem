using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Infrastructure.Data;

namespace RestaurantManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MenuItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenuItems()
    {
        var menuItems = await _context.MenuItems
            .Include(m => m.Category)
            .ToListAsync();

        return Ok(menuItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItem>> GetById(int id)
    {
        var menuItem = await _context.MenuItems
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menuItem == null)
        {
            return NotFound();
        }

        return menuItem;
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> Create(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = menuItem.Id },
            menuItem
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MenuItem menuItem)
    {
        if (id != menuItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(menuItem).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
        {
            return NotFound();
        }

        _context.MenuItems.Remove(menuItem);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}