using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Ergenekon.Application.Storage;

public interface IFileStorage
{
    string GetFileName();

    string GetRelativePath(string ext);

    string GetAbsolutePath(string objectName);

    Task<FileUploadResult> UploadObjectAsync(Stream stream, string objectName, string contentType, CancellationToken cancellationToken);

    Task<FileUploadResult> UploadObjectAsync(byte[] data, string objectName, string contentType, CancellationToken cancellationToken);

    Task<byte[]> GetBytesAsync(string objectName, CancellationToken cancellationToken);

    Task<FileObject> GetFileObjectAsync(string objectName, CancellationToken cancellationToken);
}

public class FileStorage : IFileStorage
{
    private readonly IMinioClient _minioClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<FileStorage> _logger;

    private readonly string _bucketName;

    public FileStorage(IMinioClient minioClient, IConfiguration configuration, ILogger<FileStorage> logger)
    {
        _minioClient = minioClient;
        _configuration = configuration;
        _logger = logger;

        _bucketName = _configuration["MinioOptions:BucketName"];
    }

    private async Task CreateBucketIfNotExistsAsync(CancellationToken cancellationToken)
    {
        var beArgs = new BucketExistsArgs()
             .WithBucket(_bucketName);

        // Bu isimde bir bucket olup olmadığını denetleyelim.
        bool found = await _minioClient.BucketExistsAsync(beArgs, cancellationToken);
        if (!found)
        {
            // Bu isimde bir bucket yok, oluşturalım.
            var mbArgs = new MakeBucketArgs()
                .WithBucket(_bucketName);

            await _minioClient.MakeBucketAsync(mbArgs, cancellationToken);
        }
    }

    public string GetFileName()
    {
        // random
        //return Guid.NewGuid().ToString();

        // time
        return DateTime.Now.ToFileTime().ToString();
    }

    public string GetRelativePath(string ext)
    {
        var now = DateTime.Now;
        return $"{now.ToString("yyyy/MM/dd")}/{now.ToFileTime()}{ext}";
    }

    public string GetAbsolutePath(string objectName)
    {
        //if (_minioClient.Config.Endpoint == "patiyuva-storaging:9000")
        //return $"http://127.0.0.1:9000/{_bucketName}/{objectName}";

        return $"{_minioClient.Config.Endpoint}/{_bucketName}/{objectName}";
    }

    public async Task<FileUploadResult> UploadObjectAsync(Stream stream, string objectName, string contentType, CancellationToken cancellationToken)
    {
        try
        {
            await CreateBucketIfNotExistsAsync(cancellationToken);

            var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return new FileUploadResult { Ok = true, ObjectName = objectName };
        }
        catch (MinioException mex)
        {
            _logger.LogError(mex.Message);

            return new FileUploadResult { Ok = false, Message = mex.Message };
        }
    }

    public async Task<FileUploadResult> UploadObjectAsync(byte[] data, string objectName, string contentType, CancellationToken cancellationToken)
    {
        try
        {
            await CreateBucketIfNotExistsAsync(cancellationToken);

            var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(new MemoryStream(data))
                    .WithObjectSize(data.Length)
                    .WithContentType(contentType);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return new FileUploadResult { Ok = true, ObjectName = objectName };
        }
        catch (MinioException mex)
        {
            _logger.LogError(mex.Message);

            return new FileUploadResult { Ok = false, Message = mex.Message };
        }
    }

    public async Task<byte[]> GetBytesAsync(string objectName, CancellationToken cancellationToken)
    {
        byte[] fileContents = new byte[0];

        try
        {
            // Check whether the object exists using statObject().
            // If the object is not found, statObject() throws an exception,
            // else it means that the object exists.
            // Execution is successful.
            StatObjectArgs soArgs = new StatObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName);

            await _minioClient.StatObjectAsync(soArgs, cancellationToken);

            var ms = new MemoryStream();

            // Get input stream to have content of 'my-objectname' from 'my-bucketname'
            GetObjectArgs goArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(ms);
                });

            await _minioClient.GetObjectAsync(goArgs, cancellationToken);

            // file contents
            fileContents = ms.ToArray();
            ms.Dispose();

        }
        catch (MinioException mex)
        {
            _logger.LogError(mex.Message);
        }

        return fileContents;
    }

    public async Task<FileObject> GetFileObjectAsync(string objectName, CancellationToken cancellationToken)
    {
        byte[] fileContents = new byte[0];

        try
        {
            // Check whether the object exists using statObject().
            // If the object is not found, statObject() throws an exception,
            // else it means that the object exists.
            // Execution is successful.
            StatObjectArgs soArgs = new StatObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName);

            await _minioClient.StatObjectAsync(soArgs, cancellationToken);

            var ms = new MemoryStream();

            // Get input stream to have content of 'my-objectname' from 'my-bucketname'
            GetObjectArgs goArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(ms);
                });

            var fileObj = await _minioClient.GetObjectAsync(goArgs, cancellationToken);

            // file contents
            fileContents = ms.ToArray();
            ms.Dispose();

            MimeKit.MimeTypes.TryGetExtension(fileObj.ContentType, out string extension);

            return new FileObject
            {
                FileName = fileObj.ObjectName,
                Extension = extension,
                ContentType = fileObj.ContentType,
                Data = fileContents,
            };
        }
        catch (MinioException mex)
        {
            _logger.LogError(mex.Message);
        }

        return null;
    }

    /*
    public async Task<Stream> GetStreamAsync(string objectName)
    {
        Stream ms = new MemoryStream();

        try
        {
            // Check whether the object exists using statObject().
            // If the object is not found, statObject() throws an exception,
            // else it means that the object exists.
            // Execution is successful.
            StatObjectArgs soArgs = new StatObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName);

            await _minioClient.StatObjectAsync(soArgs);

            // Get input stream to have content of 'my-objectname' from 'my-bucketname'
            GetObjectArgs goArgs = new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(ms);
                });

            await _minioClient.GetObjectAsync(goArgs);
        }
        catch (MinioException mex)
        {
            _logger.LogError(mex.Message);
        }

        return ms;
    }
    */
}
