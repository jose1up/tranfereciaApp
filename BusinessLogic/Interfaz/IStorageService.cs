namespace GestiónDeImagenIA_Back.BusinessLogic.Interfaz
{
    public interface IStorageService
    {
        Task<List<string>> ListBuckets();
        Task UploadFileAsync(string bucketName, string key, string filePath);
        
    }
    

}
