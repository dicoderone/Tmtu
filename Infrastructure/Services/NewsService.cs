using Infrastructure.Entities;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Npgsql;

public class NewsService : INewsService
{
    private readonly string _connectionString;
    private readonly IFileService _fileService;

    public NewsService(IConfiguration config, IFileService fileService)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
        _fileService = fileService;
    }

    public async Task<long> CreateAsync(NewsCreateRequest request)
    {
        var imagePath = await _fileService.SaveAsync(request.Image, "images");
        var videoPath = await _fileService.SaveAsync(request.Video, "videos");

        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand(@"
            INSERT INTO app_news (Title, Content, ImagePath, VideoPath)
            VALUES (@title, @content, @image, @video)
            RETURNING Id;
        ", conn);

        cmd.Parameters.AddWithValue("title", request.Title);
        cmd.Parameters.AddWithValue("content", request.Content);
        cmd.Parameters.AddWithValue("image", imagePath ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("video", videoPath ?? (object)DBNull.Value);

        var id = (long)await cmd.ExecuteScalarAsync();
        return id;
    }

    public async Task<List<NewsResponse>> GetAllAsync()
    {
        var list = new List<NewsResponse>();
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand(
            "SELECT Id, Title, Content, ImagePath, VideoPath FROM app_news ORDER BY CreatedAt DESC", conn
        );

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new NewsResponse
            {
                Id = reader.GetInt64(0),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                VideoUrl = reader.IsDBNull(4) ? null : reader.GetString(4)
            });
        }

        return list;
    }

    public async Task<NewsResponse?> GetByIdAsync(long id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand(
            "SELECT Id, Title, Content, ImagePath, VideoPath FROM app_news WHERE Id=@id", conn
        );
        cmd.Parameters.AddWithValue("id", id);

        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new NewsResponse
            {
                Id = reader.GetInt64(0),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                ImageUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                VideoUrl = reader.IsDBNull(4) ? null : reader.GetString(4)
            };
        }

        return null;
    }

    public async Task DeleteAsync(long id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand("DELETE FROM app_news WHERE Id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        await cmd.ExecuteNonQueryAsync();
    }
}

