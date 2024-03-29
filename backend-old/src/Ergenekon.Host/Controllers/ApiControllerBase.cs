﻿using Ergenekon.Host.Extensions;
using Ergenekon.Host.Filters;
using Ergenekon.Host.Models;
using Ergenekon.Host.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected UploadResult UploadImage(IFormFile formFile)
    {
        var imageProcessor = HttpContext.RequestServices.GetRequiredService<IImageProcessor>;

        return new UploadResult(true) { FileName = "sample.jpg" };
    }

    protected void ResizeImageAsync()
    {

    }
}

