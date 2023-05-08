using Microsoft.AspNetCore.Mvc;

namespace Angular.Controllers;

public interface IGenericController<D>
{
    Task<ActionResult<IEnumerable<D>>> GetEntitiesAsync();
    Task<ActionResult<D>> GetEntityAsync(int id);

    Task<IActionResult> CreateEntityAsync(D entity);

    Task<IActionResult> UpdateEntityAsync(int id, D entity);

    Task<IActionResult> DeleteEntityAsync(int id);
}