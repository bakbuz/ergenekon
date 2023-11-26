using Ergenekon.Application.Logging.Commands.CreateRequestLog;
using Ergenekon.Domain.Entities;
using System.Diagnostics;
using System.Text;

namespace Ergenekon.Host.Services;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            if (httpContext.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                var stopWatch = Stopwatch.StartNew();
                var requestTime = DateTime.UtcNow;
                var requestBodyContent = await ReadRequestBody(httpContext.Request);
                var originalBodyStream = httpContext.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    var response = httpContext.Response;
                    response.Body = responseBody;
                    await _next(httpContext);
                    stopWatch.Stop();

                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(response);
                    await responseBody.CopyToAsync(originalBodyStream);

                    if (httpContext.Request.Path.Value != "/api/values")
                    {
                        await SafeLog(requestTime,
                            stopWatch.ElapsedMilliseconds,
                            response.StatusCode,
                            httpContext.Request.Method,
                            //httpContext.Request.Host.HasValue ? httpContext.Request.Host.Value : null,
                            null,
                            httpContext.Request.Path,
                            httpContext.Request.QueryString.ToString(),
                            requestBodyContent,
                            responseBodyContent);
                    }
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
        catch (Exception ex)
        {
            Serilog.Log.Error(ex, "requestlog hizmeti hata");
            await _next(httpContext);
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        //request.EnableRewind();
        HttpRequestRewindExtensions.EnableBuffering(request);

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }

    private async Task SafeLog(DateTime requestTime, long responseMillis, int statusCode, string method,
                  string host, string path, string queryString, string requestBody, string responseBody)
    {
        var logEntry = new RequestLog
        {
            RequestTime = requestTime,
            ResponseMillis = responseMillis,
            StatusCode = statusCode,
            Method = method,
            Host = host,
            Path = path,
            QueryString = queryString,
            RequestBody = requestBody,
            ResponseBody = responseBody,
        };
        new CreateRequestLogCommand(logEntry);
    }
}
