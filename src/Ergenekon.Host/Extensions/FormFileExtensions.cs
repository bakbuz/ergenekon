namespace Ergenekon.Host.Extensions;

public static class FormFileExtensions
{
    private static readonly string[] AllowedImageTypes = new[] { "jpg", "png", "gif", "tiff" };

    public static byte[] GetContentBytes(this IFormFile formFile)
    {
        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public static async Task<byte[]> GetContentBytesAsync(this IFormFile formFile)
    {
        await using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public static UploadResult Validate(this IFormFile formFile)
    {
        if (formFile.Length > 0)
        {
            var maxFileSize = FromMegaBytes(2);
            if (formFile.Length > maxFileSize)
                return new UploadResult("En fazla 2MB boyutundaki görselleri karşıya yükleyebilirsiniz.");

            var ContentBytes = GetContentBytes(formFile);
            if (ContentBytes.Length > 0)
            {

            }
        }

        return new UploadResult(true);
    }

    private static long FromMegaBytes(long mb)
    {
        return mb * 1024l * 1024l;
    }
}

public class UploadResult
{
    public UploadResult(bool ok)
    {
        Ok = ok;
    }

    public UploadResult(string message)
    {
        Message = message;
    }

    public bool Ok { get; set; }
    public string? FileName { get; set; }
    public string? Message { get; set; }
}