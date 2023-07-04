using Ergenekon.Host.Filters;
using Ergenekon.Host.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected UploadResult UploadImage(IFormFile formFile)
    {
        var imageProcessor = HttpContext.RequestServices.GetRequiredService<IImageProcessor>;

        return new UploadResult { Ok = true, FileName = "sample.jpg" };
    }

    protected void ResizeImageAsync()
    {

    }
}

public class UploadResult
{
    public bool Ok { get; set; }
    public string? FileName { get; set; }
    public string? Message { get; set; }
}
