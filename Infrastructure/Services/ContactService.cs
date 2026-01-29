using Infrastructure.ApplicationDbContext;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Tmtu.Mvc.Models;

namespace Infrastructure.Services;

public class ContactService : IContactService
{
    private readonly AppDbContext _context;

    public ContactService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(ContactMessage model)
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }
}
