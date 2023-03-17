using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Ergenekon.API.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public static ErrorResponse New(ModelStateDictionary modelState)
        {
            var sb = new StringBuilder();
            foreach (var msd in modelState.Values)
            {
                foreach (var err in msd.Errors)
                {
                    sb.Append(err.ErrorMessage);
                    sb.AppendLine();
                }
            }
            return new ErrorResponse(sb.ToString());
        }

        public static ErrorResponse New(IdentityResult identityResult)
        {
            var sb = new StringBuilder();
            foreach (var err in identityResult.Errors)
            {
                sb.Append(err.Description);
                sb.AppendLine();
            }
            return new ErrorResponse(sb.ToString());
        }
    }
}
