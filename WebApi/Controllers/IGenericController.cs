using MyShop.WebApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MyShop.WebApi.Controllers;

public interface IGenericController<D> where D : BaseDto
{
    Task<ActionResult<D>> GetByIdAsync(long id);

    Task<IActionResult> CreateAsync(D entity);

    Task<IActionResult> UpdateAsync(long id, D entity);

    Task<IActionResult> DeleteAsync(long id);

    Task<IActionResult> PartiallyUpdateAsync(long id, JsonPatchDocument<D> patchDocument);
}