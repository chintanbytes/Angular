using MyShop.WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyShop.WebApi.ResourceParameters;

namespace MyShop.WebApi.Controllers;

public interface IGenericController<D, TId> where D : BaseDto
{
    Task<ActionResult<IEnumerable<D>>> GetEntitiesAsync(BaseResourceParameters parameters);

    Task<ActionResult<D>> GetEntityAsync(TId id);

    Task<IActionResult> CreateEntityAsync(D entity);

    Task<IActionResult> UpdateEntityAsync(TId id, D entity);

    Task<IActionResult> DeleteEntityAsync(TId id);

    Task<IActionResult> PartiallyUpdateEntityAsync(TId id, JsonPatchDocument<D> patchDocument);
}