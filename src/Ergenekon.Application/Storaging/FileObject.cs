namespace Ergenekon.Application.Storaging;

public class FileObject
{
    public string FileName { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
}
