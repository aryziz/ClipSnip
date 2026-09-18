using System;
using System.Linq;
using ClipSnip.Data;
using ClipSnip.Models;
using Microsoft.EntityFrameworkCore;

namespace ClipSnip.Services;

public class RecommendationService : IRecommendationService
{
    private readonly FaceShapeServices _faceShapeService;
    private readonly HairstyleRepository _repository;
    private readonly ApplicationDbContext? _db;

    public RecommendationService(
        FaceShapeServices faceShapeService,
        HairstyleRepository repository,
        ApplicationDbContext? db = null)
    {
        _faceShapeService = faceShapeService;
        _repository = repository;
        _db = db;
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
        var recommendation = recommendations.FirstOrDefault();

        return new HairstyleRecommendationsViewModel
        {
            FaceShape = faceShape,
            HaircutName = recommendation?.Name ?? string.Empty,
            HaircutSubtitle = recommendation?.Subtitle ?? recommendation?.Description ?? string.Empty,
            EstimatedTime = GetEstimatedTime(recommendation),
            BarberNotes = recommendation?.BarberNotes ?? new List<string>(),
            ImagePath = recommendation?.ImagePath ?? "/img/hair-silhouette.jpg",
            Reasons = reasons,
            Recommendations = recommendations
        };
    }

    private string GetEstimatedTime(Hairstyle? recommendation)
    {
        if (recommendation is null)
        {
            return string.Empty;
        }

        if (_db is null)
        {
            return recommendation.EstimatedTime;
        }

        var averageDuration = _db.Appointments
            .AsNoTracking()
            .Where(appointment =>
                appointment.DurationInMinutes > 0 &&
                appointment.Hairstyle != null &&
                appointment.Hairstyle.ToLower() == recommendation.Name.ToLower())
            .Select(appointment => (double?)appointment.DurationInMinutes)
            .Average();

        return averageDuration.HasValue
            ? $"{Math.Round(averageDuration.Value, MidpointRounding.AwayFromZero):0} min average"
            : recommendation.EstimatedTime;
    }
}
