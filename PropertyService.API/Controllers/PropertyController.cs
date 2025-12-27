using Microsoft.AspNetCore.Mvc;
using PropertyService.Business.Interfaces;
using PropertyService.Shared.dtos;

namespace PropertyService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PropertyDto> properties = await propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PropertyDto? p = await propertyService.GetPropertyByIdAsync(id);
            if (p == null)
                return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePropertyDto property)
        {
            await propertyService.AddAsync(property);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdatePropertyDto property)
        {
            await propertyService.UpdateAsync(id, property);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await propertyService.DeleteAsync(id);
            return Ok();
        }
    }
}

