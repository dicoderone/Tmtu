using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces;

public interface IFileService
{
    Task<string?> SaveAsync(IFormFile? file, string folder);

    Task DeleteAsync(string filePath);
}
