using Angular.Models;
using Angular.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<D, T> : ControllerBase, IGenericController<D> where D : BaseDto
{
    private readonly ILogger<IGenericController<D>> logger;
    private readonly IGenericRepository<T> repository;
    private readonly IMapper mapper;

    public GenericController(ILogger<IGenericController<D>> logger, IGenericRepository<T> repository, IMapper mapper) : base()
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all entities of type T 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<D>>> GetEntitiesAsync()
    {
        var result = await repository.GetAllAsync();
        if (result.Success)
        {
            var dto = mapper.Map<IEnumerable<D>>(result.Data);
            return Ok(dto);
        }

        return NoContent();
    }

    /// <summary>
    /// Get entity of type T by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <returns>T</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<D>> GetEntityAsync([FromRoute] int id)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound();
        }
        var dto = mapper.Map<D>(result.Data);
        return Ok(dto);
    }

    /// <summary>
    /// Create entity of type T
    /// </summary>
    /// <param name="entity">T</param>
    /// <returns>T</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEntityAsync([FromBody] D entity)
    {
        var entityModel = mapper.Map<T>(entity);
        var result = await repository.CreateAsync(entityModel);
        if (result.Success)
        {
            var dto = mapper.Map<D>(result.Data);
            return CreatedAtAction("GetEntityAsync", new { id = GetId(result.Data) }, dto);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update entity of type T by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <param name="entity">T</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEntityAsync([FromRoute] int id, [FromBody] D entity)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound();
        }

        mapper.Map(entity, result.Data);

        result = await repository.UpdateAsync(id, result.Data);

        if (result.Success)
            return NoContent();
        else
            return BadRequest();
    }

    /// <summary>
    /// Partitally update the entity of type T by it's Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateEntityAsync([FromRoute] int id, JsonPatchDocument<D> patchDocument)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound();
        }

        var entity = mapper.Map<D>(result.Data);
        patchDocument.ApplyTo(entity);

        mapper.Map(entity, result.Data);

        result = await repository.UpdateAsync(id, result.Data);

        if (result.Success)
            return NoContent();
        else
            return BadRequest();
    }

    /// <summary>
    /// Delete entity of type T by it's Id 
    /// </summary>
    /// <param name="id">T Id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntityAsync([FromRoute] int id)
    {
        var result = await repository.DeleteAsync(id);
        if (!result.Success)
        {
            return Ok();
        }
        return BadRequest();
    }

    /// <summary>
    /// Helper method to extract the entity ID
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual string GetId(T entity)
    {
        // Implement this method in derived classes to extract the entity ID
        // Example: return entity.Id;
        throw new System.NotImplementedException();
    }
}
