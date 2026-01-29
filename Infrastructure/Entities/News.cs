using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class News
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string? ImagePath { get; set; }
    public string? VideoPath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class NewsResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
}

// Yangilikni yangilash uchun DTO
public class UpdateNewsDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Author { get; set; }

    [StringLength(50)]
    public string? Category { get; set; }

    public bool IsPublished { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [StringLength(500)]
    public string? VideoUrl { get; set; }

    public IFormFile? ImageFile { get; set; }
    public IFormFile? VideoFile { get; set; }
}

// Response uchun DTO
public class NewsCreateRequest
{
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public IFormFile? Image { get; set; }
    public IFormFile? Video { get; set; }
}

// API Response wrapper
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }

    public ApiResponse()
    {
        Errors = new List<string>();
    }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Muvaffaqiyatli")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, List<string> errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}
