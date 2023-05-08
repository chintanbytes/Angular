using Angular.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers;

public interface IGenericController<D> where D : BaseDto
{
    Task<ActionResult<IEnumerable<D>>> GetEntitiesAsync();

    Task<ActionResult<D>> GetEntityAsync(int id);

    Task<IActionResult> CreateEntityAsync(D entity);

    Task<IActionResult> UpdateEntityAsync(int id, D entity);

    Task<IActionResult> DeleteEntityAsync(int id);

    Task<IActionResult> PartiallyUpdateEntityAsync(int id, JsonPatchDocument<D> patchDocument);
}