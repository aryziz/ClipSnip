using Microsoft.Extensions.Logging;
using ClipSnip.Controllers;
using ClipSnip.Models;
using ClipSnip.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ClipSnip.Tests;

public class HairstyleFinderControllerTests
{
    [Fact]
    public void Analyze_ReturnsPartialView_WithViewModel()
    {
        var mockSvc = new Mock<IRecommendationService>();
        var mockFaceShape = new Mock<FaceShapeServices>();
        var mockLogger = new Mock<ILogger<HairstyleFinderController>>();
     
  

        var expectedVm = new HairstyleRecommendationsViewModel
        {
            FaceShape = "Oval",
            Reasons = new List<string> { "Reason1" }
        };

        mockSvc
            .Setup(s => s.GetRecommendations(It.IsAny<FaceAnalysisRequest>()))
            .Returns(expectedVm);

        var controller = new HairstyleFinderController(mockSvc.Object, mockFaceShape.Object, mockLogger.Object);

        var request = new FaceAnalysisRequest
        {
            Data = new FaceRatios
            {
                LengthToWidth = 1.3,
                ForeheadToCheekbone = 0.9,
                JawToCheekbone = 0.85
            }
        };

        var actionResult = controller.Analyze(request);

        var partialResult = Assert.IsType<PartialViewResult>(actionResult);
        Assert.Equal("_ResultsPartial", partialResult.ViewName);
        Assert.Same(expectedVm, partialResult.Model);

    }
}