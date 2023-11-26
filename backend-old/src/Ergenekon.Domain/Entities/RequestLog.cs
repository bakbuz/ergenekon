namespace Ergenekon.Domain.Entities;

public class RequestLog : BaseEntity<ulong>
{
    public DateTime RequestTime { get; set; }

    public long ResponseMillis { get; set; }

    public int StatusCode { get; set; }

    public string Method { get; set; }

    public string Host { get; set; }

    public string Path { get; set; }

    public string QueryString { get; set; }

    public string RequestBody { get; set; }

    public string ResponseBody { get; set; }
}
