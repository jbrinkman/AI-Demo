using Microsoft.AspNetCore.Mvc;
using WebResources.Application;
using System.Threading.Tasks;
using System.Linq;
using WebResources.Application.Interfaces;
using WebResources.Application.DTOs;

[ApiController]
[Route("api/webresources")]
public class WebResourcesController : ControllerBase
{
    private readonly IWebResourceRepository _repository;

    public WebResourcesController(IWebResourceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _repository.GetAllAsync();
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resource = await _repository.GetByIdAsync(id);
        if (resource == null)
        {
            return NotFound();
        }
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WebResourceDto webResource)
    {
        // Basic validation
        if (webResource == null)
        {
            return BadRequest();
        }

        if (!new[] { "Web", ".Net", "AI" }.Contains(webResource.Category))
        {
            return BadRequest("Invalid category.");
        }

        await _repository.AddAsync(webResource);
        return CreatedAtAction(nameof(GetById), new { id = webResource.Id }, webResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WebResourceDto webResource)
    {
        if (id != webResource.Id)
        {
            return BadRequest("Resource ID mismatch.");
        }

        var existingResource = await _repository.GetByIdAsync(id);
        if (existingResource == null)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(webResource);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingResource = await _repository.GetByIdAsync(id);
        if (existingResource == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
