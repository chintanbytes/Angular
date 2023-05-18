using MyShop.WebApi.Models;
using MyShop.WebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using MyShop.WebApi.ResourceParameters;
using System.Text.Json;

namespace MyShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<D, T, TId> : ControllerBase, IGenericController<D, TId> where D : BaseDto
{
    private readonly ILogger<IGenericController<D, TId>> logger;
    private readonly IGenericRepository<T, TId> repository;
    private readonly IMapper mapper;

    public GenericController(ILogger<IGenericController<D, TId>> logger,
    IGenericRepository<T, TId> repository, IMapper mapper) : base()
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
        this.logger.LogInformation("GenericController<{D}, {T}, {TId}> created", typeof(D).Name, typeof(T).Name, typeof(TId).Name);
    }

    /// <summary>
    /// Get all entities of type T 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<D>>> GetEntitiesAsync([FromQuery] BaseResourceParameters parameters)
    {
        var result = await repository.GetAllAsync(parameters);
        if (result.Success)
        {
            int? PreviousPage = result.Data.HasPrevious ? result.Data.CurrentPage - 1 : null;
            int? NextPage = result.Data.HasNext ? result.Data.CurrentPage + 1 : null;

            var paginationMetadata = new
            {
                TotalCount = result.Data.TotalCount,
                PageSize = result.Data.PageSize,
                CurrentPage = result.Data.CurrentPage,
                TotalPages = result.Data.TotalPages,
                PreviousPage = PreviousPage,
                NextPage = NextPage
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var dto = mapper.Map<IEnumerable<D>>(result.Data);
            return Ok(dto);
        }

        return NoContent();
    }

    // protected string? createEntitiesResourceUri(BaseResourceParameters parameters, ResourceUriType uriType)
    // {
    //     switch (uriType)
    //     {
    //         case ResourceUriType.PreviousPage:
    //             return Url.Link("Get" + nameof(T), new { pageNumber = parameters.PageNumber - 1, pageSize = parameters.PageSize });
    //         case ResourceUriType.NextPage:
    //             return Url.Link("Get" + nameof(T), new { pageNumber = parameters.PageNumber + 1, pageSize = parameters.PageSize });
    //         default:
    //             return Url.Link("Get" + nameof(T), new { pageNumber = parameters.PageNumber, pageSize = parameters.PageSize });
    //     }
    // }

    /// <summary>
    /// Get entity of type T by it's Id
    /// </summary>
    /// <param name="id">T Id</param>
    /// <returns>T</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<D>> GetEntityAsync([FromRoute] TId id)
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
    public async Task<IActionResult> UpdateEntityAsync([FromRoute] TId id, [FromBody] D entity)
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
    public async Task<IActionResult> PartiallyUpdateEntityAsync([FromRoute] TId id, JsonPatchDocument<D> patchDocument)
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
    public async Task<IActionResult> DeleteEntityAsync([FromRoute] TId id)
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
    protected virtual TId GetId(T entity)
    {
        // Implement this method in derived classes to extract the entity ID
        // Example: return entity.Id;
        throw new System.NotImplementedException();
    }

    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelSteteDictionary)
    {
        var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
        return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
    }

}