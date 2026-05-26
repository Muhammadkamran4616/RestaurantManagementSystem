using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _service;
    private readonly IMapper _mapper;

    public MenuItemsController(
        IMenuItemService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenuItems()
    {
        var menuItems = await _service.GetAllAsync();

        var result = _mapper.Map<List<MenuItemDto>>(menuItems);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemDto>> GetMenuItem(int id)
    {
        var menuItem = await _service.GetByIdAsync(id);

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
        var createdItem = await _service.CreateAsync(menuItem);

        return CreatedAtAction(
            nameof(GetMenuItem),
            new { id = createdItem.Id },
            createdItem);
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

        var updated = await _service.UpdateAsync(menuItem);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}