using ClipSnip.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClipSnip.Controllers;

[Route("hairstyle-finder")]
public class HairstyleFinderController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("analyze")]
    public IActionResult Analyze([FromBody] FaceAnalysisRequest request)
    {
        // Later:
        // var recommendations =
        //     _recommendationService.GetRecommendations(request);

        Console.WriteLine(request.Result);
        return Ok(new
        {
            faceShape = request.Result
        });
    }
}
