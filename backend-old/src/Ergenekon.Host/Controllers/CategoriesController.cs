using Ergenekon.Application.Catalog.Categories.Commands.CreateCategory;
using Ergenekon.Application.Catalog.Categories.Commands.DeleteCategory;
using Ergenekon.Application.Catalog.Categories.Commands.UpdateCategory;
using Ergenekon.Application.Catalog.Categories.Queries.GetCategories;
using Ergenekon.Application.Catalog.Categories.Queries.GetCategoriesByParentId;
using Ergenekon.Application.Catalog.Categories.Queries.GetCategoryById;
using Ergenekon.Application.Catalog.Categories.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[Authorize]
public class CategoriesController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesVm>> GetCategories()
    {
        return Ok(await Mediator.Send(new GetCategoriesQuery()));
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesVm>> GetCategory([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new GetCategoryByIdQuery(id)));
    }

    [HttpGet("{id}/children")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoriesVm>> GetChildrenCategories([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new GetCategoriesByParentIdQuery(id)));
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

    [HttpPut("{id}/images")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddImage(int id, IFormFile image)
    {
        var uploadResult = UploadImage(image);
        if (!uploadResult.Ok)
            return BadRequest(new ResponseErrors(uploadResult.Message));

        await Mediator.Send(new AddCategoryImageCommand(id, uploadResult.FileName));

        return NoContent();
    }

    [HttpDelete("{id}/images")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveImage(int id)
    {
        await Mediator.Send(new RemoveCategoryImageCommand(id));

        return NoContent();
    }
}
