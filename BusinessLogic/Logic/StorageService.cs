using Amazon.S3;
using DotNetEnv;
using GestiónDeImagenIA_Back.BusinessLogic.Interfaz;

namespace GestiónDeImagenIA_Back.BusinessLogic.Logic;

public class StorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string  _MinioEndPoint;
    private readonly string  _AccesKeyMinio;
    private readonly string  _SecreKeyMinio;

    public StorageService()
    {
        _MinioEndPoint = Environment.GetEnvironmentVariable("MinioEndPoint");
        _AccesKeyMinio = Environment.GetEnvironmentVariable("AccesKeyMinio");
        _SecreKeyMinio = Environment.GetEnvironmentVariable("SecreKeyMinio");

        var config = new AmazonS3Config
        {
            ServiceURL = _MinioEndPoint,
            ForcePathStyle = true,

        };
        
        _s3Client = new AmazonS3Client(_AccesKeyMinio,_SecreKeyMinio,config);
        
    }


    public async Task<List<string>> ListBuckets()
    {
        try
        {
            var response = await _s3Client.ListBucketsAsync();
            return response.Buckets.Select(b=> b.BucketName).ToList();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task UploadFileAsync(string bucketName, string key, string filePath)
    {
        throw new NotImplementedException();
    }
}