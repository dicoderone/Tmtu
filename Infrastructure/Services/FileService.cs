using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string?> SaveAsync(IFormFile? file, string folder)
    {
        if (file == null || file.Length == 0)
            return null;

        // Papka yo‘li
        var uploadsFolder = Path.Combine(_env.WebRootPath, folder);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Unique file name
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        // Relative path qaytadi (client uchun URL)
        return $"/{folder}/{fileName}";
    }

    public Task DeleteAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return Task.CompletedTask;

        var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}
