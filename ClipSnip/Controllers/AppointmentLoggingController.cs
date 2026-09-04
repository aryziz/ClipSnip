using ClipSnip.Data;
using ClipSnip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClipSnip.Controllers;

[Authorize]
[Route("log-appointment")]
public class AppointmentLoggingController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        ViewBag.hairstyles = new[] { "hair1", "hair2" };
        return View();
    }

    [HttpPost("log")]
    public async Task<IActionResult> LogAppointment([FromBody] AppointmentRequest appointment)
    {
        Console.WriteLine("Postrequist");
        Console.WriteLine(appointment.date);
        var user = await userManager.GetUserAsync(User);
        var app = new Appointment();
        app.UserId = user.Id;
        Console.WriteLine($"duration = {appointment.duration}");
        app.DurationInMinutes = appointment.duration;
        app.TimeOfAppointment = appointment.date;
        app.Hairstyle = appointment.hairstyle;
        Console.WriteLine();
        Console.WriteLine(appointment.hairstyle);
        Console.WriteLine();
        user.Appointments.Add(app);
        await db.SaveChangesAsync();
        Console.WriteLine("success?");

        return View();
    }
}

public class AppointmentRequest
{
    public int duration { get; set; }
    public DateTime date { get; set; }
    public string hairstyle { get; set; }
}
