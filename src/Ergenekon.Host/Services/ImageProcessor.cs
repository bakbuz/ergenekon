using SkiaSharp;
using System.Drawing;

namespace Ergenekon.Host.Services;

public interface IImageProcessor
{
    byte[] ProcessImage(Stream stream, int maxSize);

    byte[] ProcessImage(ReadOnlySpan<byte> buffer, int maxSize);
}

public class SkiaSharpImageProcessor : IImageProcessor
{
    const int quality = 100;

    private Size CalculateDimensions(SKBitmap original, int maxSize)
    {
        int width, height;
        if (original.Width > original.Height)
        {
            width = maxSize;
            height = original.Height * maxSize / original.Width;
        }
        else
        {
            width = original.Width * maxSize / original.Height;
            height = maxSize;
        }
        return new Size(width, height);
    }

    public byte[] ProcessImage(Stream stream, int maxSize)
    {
        using (var inputStream = new SKManagedStream(stream))
        {
            using (SKBitmap original = SKBitmap.Decode(inputStream))
            {
                var size = CalculateDimensions(original, maxSize);

                using (var resized = original.Resize(new SKImageInfo(size.Width, size.Height), SKFilterQuality.High))
                {
                    if (resized != null)
                    {
                        using (var image = SKImage.FromBitmap(resized))
                        {
                            image.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
                        }
                    }
                }
            }
        }

        return new byte[0];
    }

    public byte[] ProcessImage(ReadOnlySpan<byte> buffer, int maxSize)
    {
        using (SKBitmap original = SKBitmap.Decode(buffer))
        {
            var size = CalculateDimensions(original, maxSize);

            using (var resized = original.Resize(new SKImageInfo(size.Width, size.Height), SKFilterQuality.High))
            {
                if (resized != null)
                {
                    using (var image = SKImage.FromBitmap(resized))
                    {
                        return image.Encode(SKEncodedImageFormat.Jpeg, quality).ToArray();
                    }
                }
            }
        }

        return Array.Empty<byte>();
    }
}
