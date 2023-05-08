using Angular.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<D, T> : ControllerBase where T : class where D : class
{
    private readonly ILogger<T> logger;
    private readonly GenericRepository<T> repository;
    private readonly IMapper mapper;

    public GenericController(ILogger<T> logger, GenericRepository<T> repository, IMapper mapper) : base()
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Get all entities of type <ref name="T"/> 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
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
    /// Get entity of type <ref name="T"/> by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <returns>T</returns>
    [HttpGet("{id}", Name = "Get" + nameof(D) + "ById")]
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
    /// Create entity of type <ref name="T"/>
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
            return CreatedAtRoute("Get" + nameof(T) + "ById", new { id = GetId(result.Data) }, dto);
        }

        return BadRequest();
    }

    /// <summary>
    /// Update entity of type <ref name="T"/> by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <param name="entity">T</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEntityAsync([FromRoute] int id, [FromBody] D entity)
    {
        var entityModel = mapper.Map<T>(entity);
        var result = await repository.UpdateAsync(id, entityModel);
        if (result.Success)
            return Ok();
        else
            return BadRequest();
    }

    /// <summary>
    /// Delete entity of type <ref name="T"/> by it's Id 
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
    protected virtual int GetId(T entity)
    {
        // Implement this method in derived classes to extract the entity ID
        // Example: return entity.Id;
        throw new System.NotImplementedException();
    }
}
