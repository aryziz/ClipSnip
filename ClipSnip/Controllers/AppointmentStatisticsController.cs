using ClipSnip.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClipSnip.Controllers;

[Route("statistics")]
public class AppointmentStatisticsController(ApplicationDbContext db) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {

        var avarageTimes = db.Appointments
            .GroupBy(appointment => appointment.Hairstyle, a => a.DurationInMinutes)
            .Select(a => new
            {
                hairstyle = a.Key,
                duration = a.Average()
            }
        );
        foreach (var item in avarageTimes)
        {
            Console.WriteLine($"{item.hairstyle}: {item.duration}");
        }

        ViewBag.avarageTimes = avarageTimes;

        return View();
    }
}
