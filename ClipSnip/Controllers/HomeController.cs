using ClipSnip.Data;
using ClipSnip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ClipSnip.Controllers
{
    public class HomeController (ApplicationDbContext db, UserManager<ApplicationUser> userManager) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> LogAppointment(Appointment appointment)
        {
            var user = await userManager.GetUserAsync(User);
            user.Appointments.Add(appointment);
            await db.SaveChangesAsync();

            return View();
        }
    }
}
