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

    /// <summary>
    /// Retrieves all web resources.
    /// </summary>
    /// <returns>A list of web resources.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _repository.GetAllAsync();
        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a web resource by ID.
    /// </summary>
    /// <param name="id">The ID of the web resource to retrieve.</param>
    /// <returns>The web resource with the specified ID.</returns>
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

    /// <summary>
    /// Creates a new web resource.
    /// </summary>
    /// <param name="webResourceDto">The web resource to create.</param>
    /// <returns>The created web resource.</returns>
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

    /// <summary>
    /// Updates an existing web resource.
    /// </summary>
    /// <param name="id">The ID of the web resource to update.</param>
    /// <param name="webResourceDto">The updated web resource.</param>
    /// <returns>The updated web resource.</returns>
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

    /// <summary>
    /// Deletes a web resource by ID.
    /// </summary>
    /// <param name="id">The ID of the web resource to delete.</param>
    /// <returns>No content.</returns>
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
