using Microsoft.AspNetCore.Http;

namespace BibliotecaAPI.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile file);
    }
}
