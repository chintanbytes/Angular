using MyShop.WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.WebApi.Controllers;

public interface IGenericController<D> where D : BaseDto
{
    Task<ActionResult<D>> GetEntityAsync(long id);

    Task<IActionResult> CreateEntityAsync(D entity);

    Task<IActionResult> UpdateEntityAsync(long id, D entity);

    Task<IActionResult> DeleteEntityAsync(long id);

    Task<IActionResult> PartiallyUpdateEntityAsync(long id, JsonPatchDocument<D> patchDocument);
}