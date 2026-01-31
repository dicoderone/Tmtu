using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces;

public interface INewsService
{
    Task<long> CreateAsync(NewsCreateRequest request);
    Task<List<NewsResponse>> GetAllAsync();
    Task<NewsResponse?> GetByIdAsync(long id);
    Task DeleteAsync(long id);
    Task UpdateAsync(NewsUpdateRequest request);
}
