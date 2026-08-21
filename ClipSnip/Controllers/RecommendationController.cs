using Microsoft.AspNetCore.Mvc;
using ClipSnip.Models;
using ClipSnip.Services;
using ClipSnip.Data;

namespace ClipSnip.Controllers
{
    public class RecommendationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Results()
        {
            var measurements = new FaceMeasurements
            {
                LengthToWidth = 1.40,
                ForeheadToCheekbone = 0.95,
                ForeheadToJaw = 1.05,
                JawToCheekbone = 0.90
            };

            // Find best face shape
            var faceShapeService = new FaceShapeServices();

            var faceShapeResults =
                faceShapeService.Analyse(measurements);

            var bestFaceShape =
                faceShapeResults.First();

            // Get hairstyle dummy data
            var repository = new HairstyleRepository();

            var hairstyles =
                repository.GetHairstyles();

            // Find matching hairstyles
            var recommendationService =
                new HairstyleRecommendationService();

            var recommendations =
                recommendationService.Recommend(
                    bestFaceShape.FaceShape,
                    hairstyles);

            // Send face shape data to Results.cshtml
            ViewBag.FaceShape = bestFaceShape.FaceShape;
            ViewBag.FaceShapeReasons = bestFaceShape.Reasons;

            return View(recommendations);
        }
    }
}