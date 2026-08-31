using System.Linq;
using ClipSnip.Models;
using ClipSnip.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClipSnip.Controllers;

[Route("hairstyle-finder")]
public class HairstyleFinderController : Controller
{
    private readonly IRecommendationService _recommendationService;
    private readonly FaceShapeServices _faceShapeService;

    public HairstyleFinderController(IRecommendationService recommendationService, FaceShapeServices faceShapeService)
    {
        _recommendationService = recommendationService;
        _faceShapeService = faceShapeService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("analyze")]
    public IActionResult Analyze([FromBody] FaceAnalysisRequest request)
    {
        if (request == null || request.Data == null)
        {
            return BadRequest(new { error = "Invalid request. Expected JSON with face ratios." });
        }

        var vm = _recommendationService.GetRecommendations(request);

        return PartialView("_ResultsPartial", vm);
    }
}
