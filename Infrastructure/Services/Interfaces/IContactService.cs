using Tmtu.Mvc.Models;

namespace Infrastructure.Services.Interfaces;

public interface IContactService
{
    Task<bool> AddAsync(ContactMessage model);
}
