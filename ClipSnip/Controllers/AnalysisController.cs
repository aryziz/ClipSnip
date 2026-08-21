using ClipSnip.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class AnalysisController(ILogger<AnalysisController> logger) : Controller
    {
        // GET: AnalysisController
        public ActionResult Index()
        {
            return View();
        }

        // POST: AnalysisController
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] FaceAnalysisRequest data)
        {

            if (data == null) return BadRequest("Data cannot be null");
            Console.WriteLine(data.Result);
            return Ok();
        }
    }
}
