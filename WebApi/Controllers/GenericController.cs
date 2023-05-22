using MyShop.WebApi.Dtos;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using MyShop.WebApi.Data;
using MyShop.WebApi.ResourceParameters;
using MyShop.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace MyShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class GenericController<D, T> : ControllerBase, IGenericController<D> where D : BaseDto where T : BaseEntity
{
    private readonly ILogger<IGenericController<D>> logger;
    private readonly IGenericRepository<T> repository;
    private readonly IMapper mapper;

    public GenericController(ILogger<IGenericController<D>> logger,
    IGenericRepository<T> repository, IMapper mapper) : base()
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
        this.logger.LogInformation("GenericController<{D}, {T}> created", typeof(D).Name, typeof(T).Name);
    }

    /// <summary>
    /// Get entity of type T by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <returns>T</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<D>> GetByIdAsync([FromRoute] long id)
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
    [Authorize]
    public virtual async Task<IActionResult> CreateAsync([FromBody] D entity)
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
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] D entity)
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
    [Authorize]
    public async Task<IActionResult> PartiallyUpdateAsync([FromRoute] long id, JsonPatchDocument<D> patchDocument)
    {
        var result = await repository.GetByIdAsync(id);
        if (!result.Success)
        {
            return NotFound();
        }

        var entity = mapper.Map<D>(result.Data);
        patchDocument.ApplyTo(entity, ModelState);

        if (!TryValidateModel(entity))
        {
            return ValidationProblem(ModelState);
        }

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
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
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
    protected long GetId(T entity) => entity.Id;

    protected string? createResourceUri(BaseResourceParameters parameters, ResourceUriType uriType, string methodName)
    {
        switch (uriType)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link(methodName, new { pageNumber = parameters.PageNumber - 1, pageSize = parameters.PageSize });
            case ResourceUriType.NextPage:
                return Url.Link(methodName, new { pageNumber = parameters.PageNumber + 1, pageSize = parameters.PageSize });
            default:
                return Url.Link(methodName, new { pageNumber = parameters.PageNumber, pageSize = parameters.PageSize });
        }
    }

    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelSteteDictionary)
    {
        var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
        return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
    }

}