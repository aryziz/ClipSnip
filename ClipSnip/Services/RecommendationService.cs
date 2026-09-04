using System;
using System.Linq;
using ClipSnip.Data;
using ClipSnip.Models;

namespace ClipSnip.Services;

public class RecommendationService : IRecommendationService
{
    private readonly FaceShapeServices _faceShapeService;
    private readonly HairstyleRepository _repository;

    public RecommendationService(FaceShapeServices faceShapeService, HairstyleRepository repository)
    {
        _faceShapeService = faceShapeService;
        _repository = repository;
    }

    public HairstyleRecommendationsViewModel GetRecommendations(FaceAnalysisRequest request)
    {
        if (request == null || request.Data == null)
        {
            return new HairstyleRecommendationsViewModel();
        }

        // Determine face shape: prefer client-provided result if available
        string faceShape;
        List<string> reasons = new List<string>();

        if (!string.IsNullOrWhiteSpace(request.Result))
        {
            faceShape = request.Result;
        }
        else
        {
            // Map FaceRatios to FaceMeasurements. ForeheadToJaw is not provided by FaceRatios,
            // so fall back to ForeheadToCheekbone to allow analysis to run.
            var measurements = new FaceMeasurements
            {
                LengthToWidth = request.Data.LengthToWidth,
                ForeheadToCheekbone = request.Data.ForeheadToCheekbone,
                ForeheadToJaw = request.Data.ForeheadToCheekbone,
                JawToCheekbone = request.Data.JawToCheekbone
            };

            var results = _faceShapeService.Analyse(measurements);
            var best = results.FirstOrDefault();
            faceShape = best?.FaceShape ?? string.Empty;
            if (best?.Reasons != null) reasons = best.Reasons;
        }

        var hairstyles = _repository.GetHairstyles();
        var recommendations = hairstyles
            .Where(h => h.SuitableFaceShapes
                .Any(shape => shape.Equals(faceShape, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        return new HairstyleRecommendationsViewModel
        {
            FaceShape = faceShape,
            Reasons = reasons,
            Recommendations = recommendations
        };
    }
}
