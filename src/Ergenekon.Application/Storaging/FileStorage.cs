using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Ergenekon.Application.Storaging;

public interface IFileStorage
{
    string GetFileName();

    string GetRelativePath(string ext);

    string GetAbsolutePath(string objectName);

    Task<FileUploadResult> UploadStreamAsync(Stream stream, string objectName, string contentType, long length, CancellationToken cancellationToken = default);

    Task<FileUploadResult> UploadDataAsync(byte[] data, string objectName, string contentType, CancellationToken cancellationToken);

    Task<byte[]> GetBytesAsync(string objectName, CancellationToken cancellationToken);

    Task<FileObject> GetFileObjectAsync(string objectName, CancellationToken cancellationToken);
}

public class FileStorage : IFileStorage
{
    private readonly string _bucketName;

    private readonly MinioOptions _minioOptions;
    private readonly ILogger<FileStorage> _logger;
    private readonly IMinioClient _minioClient;

    public FileStorage(IOptions<MinioOptions> options, ILogger<FileStorage> logger)
    {
        _minioOptions = options.Value;
        _logger = logger;

        _bucketName = _minioOptions.BucketName;
        _minioClient = GetMinioClient();
    }

    private IMinioClient GetMinioClient()
    {
        IMinioClient minioClient = new MinioClient()
            .WithEndpoint(_minioOptions.Endpoint)
            .WithCredentials(_minioOptions.AccessKey, _minioOptions.SecretKey)
            .WithSSL(_minioOptions.UseSSL)
            .Build();

        return minioClient;
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
        if (_minioOptions.Endpoint == "patiyuva-storaging:9000")
            return $"http://127.0.0.1:9000/{_bucketName}/{objectName}";

        return $"{_minioOptions.Endpoint}/{_bucketName}/{objectName}";
    }

    public async Task<FileUploadResult> UploadStreamAsync(Stream stream, string objectName, string contentType, long length, CancellationToken cancellationToken = default)
    {
        try
        {
            await CreateBucketIfNotExistsAsync(cancellationToken);

            var putObjectArgs = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(stream)
                    .WithObjectSize(length)
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

    public async Task<FileUploadResult> UploadDataAsync(byte[] data, string objectName, string contentType, CancellationToken cancellationToken)
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
