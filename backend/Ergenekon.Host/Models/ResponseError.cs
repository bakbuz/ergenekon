using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ergenekon.Host.Models;

public class ResponseError
{
    public ResponseError(string message, string? code = null)
    {
        Code = code;
        Message = message;
    }

    public string? Code { get; set; }

    public string Message { get; set; }
}

public class ResponseErrors
{
    public ResponseErrors(string message, string? code = null)
    {
        Errors = new ResponseError[] { new ResponseError(message, code) };
    }

    public ResponseErrors(IdentityResult result)
    {
        Errors = result.Errors.Select(s => new ResponseError(s.Description, s.Code)).ToArray();
    }

    public ResponseErrors(ModelStateDictionary modelState)
    {
        Errors = modelState.SelectMany(s => s.Value.Errors.Select(e => new ResponseError(e.ErrorMessage))).ToArray();
    }

    public ResponseErrors(ValidationResult validateResult)
    {
        Errors = validateResult.Errors.Select(s => new ResponseError(s.ErrorMessage, s.ErrorCode)).ToArray();
    }

    public ResponseError[] Errors { get; set; }
}
