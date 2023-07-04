using Ergenekon.Application.Catalog.Categories.Commands.CreateCategory;
using Ergenekon.Application.Catalog.Categories.Commands.DeleteCategory;
using Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;
using Ergenekon.Application.Catalog.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Authorize]
public class CategoriesController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesVm>> GetAll()
    {
        return Ok(await Mediator.Send(new GetCategoriesQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var id = await Mediator.Send(command);

        return Ok(id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        await Mediator.Send(command.SetId(id));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}
