using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tmtu.Mvc.Models;

namespace Tmtu.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IContactService _contactService;

    public HomeController(IContactService contactService)
    {
        _contactService = contactService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ContactMessage dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Maʼlumotlar noto‘g‘ri";
            return RedirectToAction("Index");
        }

        var result = await _contactService.AddAsync(dto);

        if (result)
            TempData["Success"] = "Xabaringiz muvaffaqiyatli yuborildi";
        else
            TempData["Error"] = "Xatolik yuz berdi";

        return RedirectToAction("Index");
    }
    public IActionResult funksiyavavazifalar() => View();
    public IActionResult aloqavaishoraatmuassasasixizmatlari() => View();
    public IActionResult elektrtaminotkorxonasixizmatlari() => View();
    public IActionResult mtutarixi() => View();
    public IActionResult rahbariyat() => View();
    public IActionResult mamuriytuzilish() => View();
    public IActionResult korxonavatashkilotlar() => View();
    public IActionResult stansiyalar() => View();
    public IActionResult tarkibiybolinmalar() => View();
    public IActionResult temiryolstansiyalarixizmatlari() => View();
    public IActionResult temiryoltamirlashkorxonalarixizmatlari() => View();  
    public IActionResult lokomotivdepokorxonasiizmatlari() => View();
    public IActionResult vagondepokorxonasixizmatlari() => View();
    public IActionResult oztemiryolhisobfilialixizmatlari() => View();
    public IActionResult yordamchixojaliklarxizmatlari() => View();
    public IActionResult Korxonafotosuratlari() => View();
    public IActionResult murojaatyuborish() => View();
    public IActionResult teztezberiladigansavollar() => View();
    public IActionResult ochiqmalumotlar() => View();
    public IActionResult tenderlar() => View();
    public IActionResult test() => View();

}
