using ClipSnip.Data;
using ClipSnip.Models;
using ClipSnip.Services;

namespace ClipSnip.Tests;

public class RecommendationServiceTests
{
    [Fact]
    public void GetRecommendations_UsesProvidedResult_ToFilterHairstyles()
    {
        var faceShapeService = new FaceShapeServices();
        var repository = new HairstyleRepository();
        var svc = new RecommendationService(faceShapeService, repository);

        var request = new FaceAnalysisRequest
        {
            Data = new FaceRatios { LengthToWidth = 1.4, ForeheadToCheekbone = 0.95, JawToCheekbone = 0.90 },
            Result = "Diamond"
        };

        var vm = svc.GetRecommendations(request);

        Assert.Equal("Diamond", vm.FaceShape);
        Assert.NotEmpty(vm.Recommendations);

        Assert.All(vm.Recommendations, h => Assert.Contains("Diamond", h.SuitableFaceShapes));
    }

    [Fact]
    public void GetRecommendations_AnalyzesRatios_WhenResultEmpty()
    {
        var faceShapeService = new FaceShapeServices();
        var repository = new HairstyleRepository();
        var svc = new RecommendationService(faceShapeService, repository);

        var request = new FaceAnalysisRequest
        {
            Data = new FaceRatios { LengthToWidth = 1.4, ForeheadToCheekbone = 0.95, JawToCheekbone = 0.90 },
            Result = string.Empty
        };

        var vm = svc.GetRecommendations(request);

        Assert.False(string.IsNullOrWhiteSpace(vm.FaceShape));
        Assert.NotEmpty(vm.Recommendations);

        Assert.Equal("Oval", vm.FaceShape);
        Assert.All(vm.Recommendations, h => Assert.Contains(vm.FaceShape, h.SuitableFaceShapes));
    }
}
