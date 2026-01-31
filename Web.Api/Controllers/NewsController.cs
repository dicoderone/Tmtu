using Infrastructure.Entities;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] NewsCreateRequest request)
    {
        var id = await _newsService.CreateAsync(request);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _newsService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
        => Ok(await _newsService.GetByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _newsService.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] NewsUpdateRequest request)
    {
        await _newsService.UpdateAsync(request);
        return NoContent();
    }

}
