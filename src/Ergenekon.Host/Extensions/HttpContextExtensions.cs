using Azure.Core;
using System.Text;

namespace Ergenekon.Host.Extensions;

public static class HttpContextExtensions
{
    public static string ReadRequestBody(this HttpContext httpContext)
    {
        if (httpContext == null) return string.Empty;

        var buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
        httpContext.Request.Body.Read(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }

    public static string ReadResponseBody(this HttpContext httpContext)
    {
        if (httpContext == null) return string.Empty;

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var bodyAsText = new StreamReader(httpContext.Response.Body).ReadToEnd();
        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }
}
