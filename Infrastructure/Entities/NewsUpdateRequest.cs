using Microsoft.AspNetCore.Http;

namespace Infrastructure.Entities;

public class NewsUpdateRequest
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public IFormFile? Video { get; set; }
}
