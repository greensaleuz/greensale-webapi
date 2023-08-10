using GreenSale.Service.Interfaces.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace GreenSale.Service.Service.Common;

public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    private readonly string IMAGES = "image";
    private readonly string ROOTPATH;

    public FileService(IWebHostEnvironment env)
    {
        ROOTPATH = env.WebRootPath;
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);

        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });

            return true;
        }

        return false;
    }

    public Task<string> UploadImageAsync(IFormFile image)
    {
        throw new NotImplementedException();
    }
}
