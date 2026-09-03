using ClipSnip.Data;
using ClipSnip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClipSnip.Controllers;

[Route("log-appointment")]
public class AppointmentLoggingController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("log")]
    public async Task<IActionResult> LogAppointment([FromBody] AppointmentRequest appointment)
    {
        Console.WriteLine("Postrequist");
        Console.WriteLine(appointment.date);
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }

        var app = new Appointment
        {
            UserId = user.Id,
            DurationInMinutes = appointment.duration,
            TimeOfAppointment = appointment.date
        };

        user.Appointments.Add(app);
        await db.SaveChangesAsync();

        return View();
    }
}

public class AppointmentRequest
{
    public int duration { get; set; }
    public DateTime date { get; set; }
}
