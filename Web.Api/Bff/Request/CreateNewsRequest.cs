using Infrastructure.Enums;

namespace Web.Api.Bff.Request;

public class CreateNewsRequest
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public ContentType ContentType { get; set; }
    public string? TextContent { get; set; }
    public IFormFile? File { get; set; }
}