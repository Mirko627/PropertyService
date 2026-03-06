using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Business.Interfaces;
using PropertyService.Shared.dtos;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PropertyDto>>> GetAll()
    {
        var properties = await _propertyService.GetAllAsync();
        return Ok(properties);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PropertyDto>> GetById(int id)
    {
        var p = await _propertyService.GetPropertyByIdAsync(id);
        return Ok(p);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] CreatePropertyDto property)
    {
        int userId = GetUserId();
        await _propertyService.AddAsync(property, userId);
        return Created();
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyDto property)
    {
        int userId = GetUserId();
        await _propertyService.UpdateAsync(id, property, userId);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        int userId = GetUserId();
        await _propertyService.DeleteAsync(id, userId);
        return Ok();
    }

    private int GetUserId()
    {
        string? userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("ID utente non trovato nel token");

        return int.Parse(userIdClaim);
    }
}