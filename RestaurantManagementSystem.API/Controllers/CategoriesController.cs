using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Interfaces;

namespace RestaurantManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;

    public CategoriesController(
        ICategoryService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _service.GetAllAsync();

        var result = _mapper.Map<List<CategoryDto>>(categories);

        return Ok(result);
    }
}