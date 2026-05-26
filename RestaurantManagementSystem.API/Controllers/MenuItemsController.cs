using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Infrastructure.Data;

namespace RestaurantManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MenuItemsController(
        ApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenuItems()
    {
        var menuItems = await _context.MenuItems
            .Include(m => m.Category)
            .ToListAsync();

        var result = _mapper.Map<List<MenuItemDto>>(menuItems);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemDto>> GetMenuItem(int id)
    {
        var menuItem = await _context.MenuItems
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menuItem == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<MenuItemDto>(menuItem);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> CreateMenuItem(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetMenuItem),
            new { id = menuItem.Id },
            menuItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(
        int id,
        MenuItem menuItem)
    {
        if (id != menuItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(menuItem).State =
            EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
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