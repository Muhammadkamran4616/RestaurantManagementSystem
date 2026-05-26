using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _service;

    public MenuItemsController(IMenuItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var menuItem = await _service.GetByIdAsync(id);

        if (menuItem == null)
            return NotFound();

        return Ok(menuItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MenuItem menuItem)
    {
        var createdItem = await _service.CreateAsync(menuItem);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdItem.Id },
            createdItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MenuItem menuItem)
    {
        if (id != menuItem.Id)
            return BadRequest();

        var updated = await _service.UpdateAsync(menuItem);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}